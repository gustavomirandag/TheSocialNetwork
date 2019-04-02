using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSocialNetwork.DomainModel.Entities;
using TheSocialNetwork.DomainModel.Interfaces.Repositories;

namespace TheSocialNetwork.DataAccess.Repositories
{
    public class ProfileStoredProcedureRepository : IProfileRepository
    {
        private SqlConnection _sqlConnection = new SqlConnection(DataAccess.
            Properties.Settings.Default.DbConnectionString);

        public void Create(Profile profile)
        {
            
            var sqlCommand = new SqlCommand("CreateProfile", _sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            _sqlConnection.Open();
            sqlCommand.Parameters.AddWithValue("Id", profile.Id);
            sqlCommand.Parameters.AddWithValue("Name", profile.Name);
            sqlCommand.Parameters.AddWithValue("Birthday", profile.Birthday);
            sqlCommand.Parameters.AddWithValue("Address", profile.Address);
            sqlCommand.Parameters.AddWithValue("PhotoUrl", profile.PhotoUrl);
            sqlCommand.Parameters.AddWithValue("Country", profile.Country);
            sqlCommand.ExecuteNonQuery();
            _sqlConnection.Close();
        }

        public void Delete(Guid post)
        {
            throw new NotImplementedException();
        }

        public Profile Read(Guid post)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Profile> ReadAll()
        {
            var profiles = new List<Profile>(); //Function result

            var sqlCommand = new SqlCommand("GetAllProfile", _sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            _sqlConnection.Open();
            var reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                //===== Preencho o CurrentProfile ======
                var currentProfile = new Profile();
                currentProfile.Id = Guid.Parse(reader["Id"].ToString());
                currentProfile.Name = reader["Name"].ToString();
                currentProfile.Birthday = DateTime.Parse(reader["Birthday"].ToString());
                currentProfile.PhotoUrl = reader["PhotoUrl"].ToString();
                //currentProfile.Friends = CRIAR STOREDPROCEDURE GetProfileFriends
                //======================================

                //Add current profile to profiles
                profiles.Add(currentProfile);
            }
            _sqlConnection.Close();
            return profiles;
        }

        public void Update(Profile profile)
        {
            throw new NotImplementedException();
        }
    }
}
