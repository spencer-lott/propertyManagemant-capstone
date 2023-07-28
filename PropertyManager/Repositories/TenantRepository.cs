using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using PropertyManager.Models;
using PropertyManager.Utils;
using System.Data;
using Microsoft.Extensions.Hosting;

namespace PropertyManager.Repositories
{
    public class TenantRepository : BaseRepository, ITenantRepository
    {
        public TenantRepository(IConfiguration configuration) : base(configuration) { }

        public List<Tenant> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, FirstName, LastName, Phone, Employment, EmergencyContactName, EmergencyContactPhone, GeneralNotes, PropertyId, UserProfileId
                        FROM Tenant
                        ORDER BY LastName ASC
                        ";

                    var reader = cmd.ExecuteReader();

                    var tenants = new List<Tenant>();
                    while (reader.Read())
                    {
                        tenants.Add(new Tenant()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            FirstName = DbUtils.GetString(reader, "FirstName"),
                            LastName = DbUtils.GetString(reader, "LastName"),
                            Phone = DbUtils.GetString(reader, "Phone"),
                            Employment = DbUtils.GetString(reader, "Employment"),
                            EmergencyContactName = DbUtils.GetString(reader, "EmergencyContactName"),
                            EmergencyContactPhone = DbUtils.GetString(reader, "EmergencyContactPhone"),
                            GeneralNotes = DbUtils.GetString(reader, "GeneralNotes"),
                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                        });
                    }

                    reader.Close();

                    return tenants;
                }
            }
        }

        public Tenant GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT t.Id AS TId, t.FirstName, t.LastName, t.Phone, t.Employment, t.EmergencyContactName, t.EmergencyContactPhone, t.GeneralNotes, t.PropertyId, t.UserProfileId, p.Id AS PId, p.StreetAddress FROM Tenant t
                        LEFT JOIN Property p ON p.id = t.PropertyId
                        WHERE t.id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);

                    var reader = cmd.ExecuteReader();

                    Tenant tenant = null;
                    if (reader.Read())
                    {
                        tenant = new Tenant()
                        {
                            Id = DbUtils.GetInt(reader, "TId"),
                            FirstName = DbUtils.GetString(reader, "FirstName"),
                            LastName = DbUtils.GetString(reader, "LastName"),
                            Phone = DbUtils.GetString(reader, "Phone"),
                            Employment = DbUtils.GetString(reader, "Employment"),
                            EmergencyContactName = DbUtils.GetString(reader, "EmergencyContactName"),
                            EmergencyContactPhone = DbUtils.GetString(reader, "EmergencyContactPhone"),
                            GeneralNotes = DbUtils.GetString(reader, "GeneralNotes"),
                            PropertyId = DbUtils.GetInt(reader, "PropertyId"),
                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                            Property = new Property()
                            {
                                Id = DbUtils.GetInt(reader, "PId"),
                                StreetAddress = DbUtils.GetString(reader, "StreetAddress")
                            }
                        };
                    }
                    reader.Close();

                    return tenant;
                }
            }
        }

        public void Add(Tenant tenant)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Tenant (FirstName, LastName, Phone, Employment, EmergencyContactName, EmergencyContactPhone, GeneralNotes, PropertyId, UserProfileId )
                        OUTPUT INSERTED.ID
                        VALUES (@FirstName, @LastName, @Phone, @Employment, @EmergencyContactName, @EmergencyContactPhone, @GeneralNotes, @PropertyId, @UserProfileId )";
                    DbUtils.AddParameter(cmd, "@FirstName", tenant.FirstName);
                    DbUtils.AddParameter(cmd, "@LastName", tenant.LastName);
                    DbUtils.AddParameter(cmd, "@Phone", tenant.Phone);
                    DbUtils.AddParameter(cmd, "@Employment", tenant.Employment);
                    DbUtils.AddParameter(cmd, "@EmergencyContactName", tenant.EmergencyContactName);
                    DbUtils.AddParameter(cmd, "@EmergencyContactPhone", tenant.EmergencyContactPhone);
                    DbUtils.AddParameter(cmd, "@GeneralNotes", tenant.GeneralNotes);
                    DbUtils.AddParameter(cmd, "@PropertyId", tenant.PropertyId);
                    DbUtils.AddParameter(cmd, "@UserProfileId", tenant.UserProfileId);
                    tenant.Id = (int)cmd.ExecuteScalar();
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
                    cmd.CommandText = "DELETE FROM Tenant WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Tenant tenant)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Tenant
                           SET FirstName = @FirstName,
                               LastName = @LastName,
                               Phone = @Phone,
                               Employment = @Employment,
                               EmergencyContactName = @EmergencyContactName,
                               EmergencyContactPhone = @EmergencyContactPhone,
                               GeneralNotes = @GeneralNotes,
                               PropertyId = @PropertyId,
                               UserProfileId = @UserProfileId

                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@FirstName", tenant.FirstName);
                    DbUtils.AddParameter(cmd, "@LastName", tenant.LastName);
                    DbUtils.AddParameter(cmd, "@Phone", tenant.Phone);
                    DbUtils.AddParameter(cmd, "@Employment", tenant.Employment);
                    DbUtils.AddParameter(cmd, "@EmergencyContactName", tenant.EmergencyContactName);
                    DbUtils.AddParameter(cmd, "@EmergencyContactPhone", tenant.EmergencyContactPhone);
                    DbUtils.AddParameter(cmd, "@GeneralNotes", tenant.GeneralNotes);
                    DbUtils.AddParameter(cmd, "@PropertyId", tenant.PropertyId);
                    DbUtils.AddParameter(cmd, "@UserProfileId", tenant.UserProfileId);
                    DbUtils.AddParameter(cmd, "@Id", tenant.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
