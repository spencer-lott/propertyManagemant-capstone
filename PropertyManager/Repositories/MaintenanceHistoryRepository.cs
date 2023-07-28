using PropertyManager.Models;
using PropertyManager.Utils;

namespace PropertyManager.Repositories
{
    public class MaintenanceHistoryRepository : BaseRepository, IMaintenanceHistoryRepository
    {
        public MaintenanceHistoryRepository(IConfiguration configuration) : base(configuration) { }

        public List<MaintenanceHistory> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, Description, DateCompleted, DateRequested, PropertyId, UserProfileId FROM MaintenanceHistory
                        ORDER BY DateCompleted DESC                        
                        ";

                    var reader = cmd.ExecuteReader();

                    var notes = new List<MaintenanceHistory>();
                    while (reader.Read())
                    {
                        notes.Add(new MaintenanceHistory()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Description = DbUtils.GetString(reader, "Description"),
                            DateCompleted = DbUtils.GetDateTime(reader, "DateCompleted"),
                            DateRequested = DbUtils.GetDateTime(reader, "DateRequested"),
                            PropertyId = DbUtils.GetInt(reader, "PropertyId"),
                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                        });
                    }

                    reader.Close();

                    return notes;
                }
            }
        }

        public MaintenanceHistory GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, Description, DateCompleted, DateRequested, PropertyId, UserProfileId FROM MaintenanceHistory
                        WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);

                    var reader = cmd.ExecuteReader();

                    MaintenanceHistory note = null;
                    if (reader.Read())
                    {
                        note = new MaintenanceHistory()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Description = DbUtils.GetString(reader, "Description"),
                            DateCompleted = DbUtils.GetDateTime(reader, "DateCompleted"),
                            DateRequested = DbUtils.GetDateTime(reader, "DateRequested"),
                            PropertyId = DbUtils.GetInt(reader, "PropertyId"),
                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                        };
                    }
                    reader.Close();

                    return note;
                }
            }
        }

        public void Add(MaintenanceHistory note)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO MaintenanceHistory (Description, DateCompleted, DateRequested, PropertyId, UserProfileId )
                        OUTPUT INSERTED.ID
                        VALUES (@Description, @DateCompleted, @DateRequested, @PropertyId, @UserProfileId)";
                    DbUtils.AddParameter(cmd, "@Description", note.Description);
                    DbUtils.AddParameter(cmd, "@DateCompleted", note.DateCompleted);
                    DbUtils.AddParameter(cmd, "@DateRequested", note.DateRequested);
                    DbUtils.AddParameter(cmd, "@PropertyId", note.PropertyId);
                    DbUtils.AddParameter(cmd, "@UserProfileId", note.UserProfileId);
                    note.Id = (int)cmd.ExecuteScalar();
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
                    cmd.CommandText = "DELETE FROM MaintenanceHistory WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(MaintenanceHistory note)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE MaintenanceHistory
                           SET Description = @Description,
                               DateCompleted = @DateCompleted,
                               DateRequested = @DateRequested,
                               PropertyId = @PropertyId,
                               UserProfileId = @UserProfileId

                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Description", note.Description);
                    DbUtils.AddParameter(cmd, "@DateCompleted", note.DateCompleted);
                    DbUtils.AddParameter(cmd, "@DateRequested", note.DateRequested);
                    DbUtils.AddParameter(cmd, "@PropertyId", note.PropertyId);
                    DbUtils.AddParameter(cmd, "@UserProfileId", note.UserProfileId);
                    DbUtils.AddParameter(cmd, "@Id", note.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
