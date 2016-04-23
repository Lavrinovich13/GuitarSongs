using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DalContracts.Models;
using System.Data;
using DalContracts.RepositoriesInterfaces;

namespace DalMsSql.Repositories
{
    public class TextRepository
        : BaseRepository, ITextRepository
    {
        public TextRepository(IDbTransaction transaction)
            : base(transaction)
        { }

        public IList<Text> GetBaseSongText(int baseSongId)
        {
            var text = Connection.Query<Text>
                (string.Format(@"SELECT TextId, TextContent, TextName
                                FROM Text
                                WHERE BaseSongId={0}", baseSongId), transaction: Transaction)
                    .ToList();

            return text;
        }

        public IList<Text> GetUserSongText(int userSongId)
        {
            var text = Connection.Query<Text>
                (string.Format(@"SELECT Text.TextId, TextContent, TextName
                                FROM
                                (SELECT TextId FROM UserSongText
                                WHERE UserSongId={0}) AS t
                                JOIN Text ON Text.TextId=t.TextId", userSongId), transaction: Transaction)
                    .ToList();

            return text;
        }

        public void AddTextToBaseSong(int baseSongId, Text text)
        {
            Connection.Execute(@"INSERT INTO Text(TextContent, BaseSongId, TextName) VALUES ('" + text.TextContent + "'," + baseSongId + ",'" + text.TextName + "')", transaction: Transaction);
        }

        public void AddToUserText(int textId, int songId)
        {
            Connection.Execute(@"INSERT INTO UserSongText(UserSongId, TextId) 
                               VALUES (" + songId + ",'" + textId + "')", transaction: Transaction);
        }

        public void DeleteTextOfUserSong(int userSongId)
        {
            var textsId = GetUserSongText(userSongId).Select(x => x.TextId).ToList();

            if (textsId != null && textsId.Count != 0)
            {
                Connection.Execute(string.Format(@"DELETE FROM UserSongText WHERE UserSongId={0}", userSongId), transaction: Transaction);
                Connection.Execute(string.Format(@"DELETE FROM Text WHERE TextId IN ({0}) AND BaseSongId=NULL", string.Join(", ", textsId.Select(x => x.ToString()).ToArray())), transaction: Transaction);
            }
        }

        public int? AddUserText(Text text, int userSongId)
        {
            var textId = Connection.Query<int>(string.Format(@"INSERT INTO Text(TextName, TextContent) VALUES('{0}', '{1}') SELECT CAST(SCOPE_IDENTITY() as int)",
                text.TextName, text.TextContent),
                transaction: Transaction)
                .SingleOrDefault();

            if(textId != null)
            {
                var userTextId = Connection.Query<int>(string.Format(@"INSERT INTO UserSongText(TextId, UserSongId) VALUES({0}, {1}) SELECT CAST(SCOPE_IDENTITY() as int)",
                    textId, userSongId), transaction: Transaction)
                    .SingleOrDefault();

                return userTextId;
            }
            return null;
        }

        public void UpdateUserText(Text text, int userSongId)
        {
            var baseSongId = Connection.Query<int>(string.Format(@"SELECT BaseSongId FROM Text WHERE TextId={0}", text.TextId),
                transaction: Transaction)
                .SingleOrDefault();

            if (baseSongId == null)
            {
                Connection.Execute(string.Format(@"UPDATE Text SET TextName='{0}', TextContent='{1}' WHERE TextId={2}",
                    text.TextName, text.TextContent, text.TextId), transaction: Transaction);
            }
            else
            {
                var newTextId = AddUserText(text, userSongId);
                DeleteUserText(text, userSongId);
            }
        }

        public void DeleteUserText(Text text, int userSongId)
        {
            Connection.Execute(string.Format(@"DELETE FROM UserSongText WHERE TextId={0} AND UserSongId={1}",
                text.TextId, userSongId), transaction: Transaction);

            Connection.Execute(string.Format(@"DELETE FROM Text WHERE TextId={0} AND BaseSongId IS NULL",
                text.TextId), transaction: Transaction);
        }
    }
}
