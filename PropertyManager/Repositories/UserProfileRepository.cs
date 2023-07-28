using PropertyManager.Models;
using PropertyManager.Utils;
using System.Data;

namespace PropertyManager.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

        public List<UserProfile> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, Email, IsEmployee, IsAdmin 
                        FROM UserProfile
                        ";

                    var reader = cmd.ExecuteReader();

                    var userProfiles = new List<UserProfile>();
                    while (reader.Read())
                    {
                        userProfiles.Add(new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Email = DbUtils.GetString(reader, "Email"),
                            IsEmployee = reader.GetBoolean(reader.GetOrdinal("IsEmployee")),
                            IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin")),
                        });
                    }

                    reader.Close();

                    return userProfiles;
                }
            }
        }

        public UserProfile GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, Email, IsEmployee, IsAdmin 
                        FROM UserProfile
                        WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);

                    var reader = cmd.ExecuteReader();

                    UserProfile userProfile = null;
                    if (reader.Read())
                    {
                        userProfile = new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Email = DbUtils.GetString(reader, "Email"),
                            IsEmployee = reader.GetBoolean(reader.GetOrdinal("IsEmployee")),
                            IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin"))

                        };

                    }
                    reader.Close();

                    return userProfile;
                }
            }
        }

        public void Add(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO UserProfile (Email, IsEmployee, IsAdmin)
                        OUTPUT INSERTED.ID
                        VALUES (@Email, @IsEmployee, @IsAdmin)";
                    DbUtils.AddParameter(cmd, "@Email", userProfile.Email);
                    DbUtils.AddParameter(cmd, "@IsEmployee", userProfile.IsEmployee);
                    DbUtils.AddParameter(cmd, "@IsAdmin", userProfile.IsAdmin);
                    userProfile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM UserProfile WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE UserProfile
                           SET Email = @Email,
                               IsEmployee = @IsEmployee,
                               IsAdmin = @IsAdmin
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Email", userProfile.Email);
                    DbUtils.AddParameter(cmd, "@IsEmployee", userProfile.IsEmployee);
                    DbUtils.AddParameter(cmd, "@IsAdmin", userProfile.IsAdmin);
                    DbUtils.AddParameter(cmd, "@Id", userProfile.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
