using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using FinalWebApi.Models;
using System.Data;
using System.Diagnostics;

namespace DamoFullPrj.Models
{
    public class DBServices
    {
         static string conString = @"workstation id=MiddelPrj.mssql.somee.com;packet size=4096;user id=oranitgerbi99_SQLLogin_1;pwd=Oo102030--;data source=MiddelPrj.mssql.somee.com;persist security info=False;initial catalog=MiddelPrj;TrustServerCertificate=True";
        public static Parent Login(string email, string pass)
        {
            Parent parent = null;
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Parents WHERE email = @Email AND passwordUserd = @PasswordUserd", con);

                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@PasswordUserd", pass);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    parent = new Parent
                    {
                        idParent = rdr["idParent"].ToString(),
                        firstName = rdr["firstName"].ToString(),
                        lastName = rdr["lastName"].ToString(),
                        email = rdr["email"].ToString(),
                        passwordUserd = rdr["passwordUserd"].ToString()
                    };
                }
                con.Close();
            }

            return parent;
        }

        public static bool AddParent(Parent parent)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Parents (idParent, firstName, lastName, email, passwordUserd) " +
                    "VALUES (@idParent, @firstName, @lastName, @email, @passwordUserd)", con);

                cmd.Parameters.AddWithValue("@idParent", parent.idParent);
                cmd.Parameters.AddWithValue("@firstName", parent.firstName);
                cmd.Parameters.AddWithValue("@lastName", parent.lastName);
                cmd.Parameters.AddWithValue("@email", parent.email);
                cmd.Parameters.AddWithValue("@passwordUserd", parent.passwordUserd);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0;
            }
        }

        public static bool EditParent(Parent parent)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Parents SET firstName = @firstName, lastName = @lastName, email = @email, passwordUserd = @passwordUserd " +
                    "WHERE idParent = @idParent", con);

                cmd.Parameters.AddWithValue("@firstName", parent.firstName);
                cmd.Parameters.AddWithValue("@lastName", parent.lastName);
                cmd.Parameters.AddWithValue("@email", parent.email);
                cmd.Parameters.AddWithValue("@passwordUserd", parent.passwordUserd);
                cmd.Parameters.AddWithValue("@idParent", parent.idParent);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0;
            }
        }

        public static bool DeleteParent(string parentId)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Parents WHERE idParent = @idParent", con);

                cmd.Parameters.AddWithValue("@idParent", parentId);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0;
            }
        }
        public static List<Parent> GetAllParents()
        {
            List<Parent> parents = new List<Parent>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Parents", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Parent parent = new Parent
                    {
                        idParent = rdr["idParent"].ToString(),
                        firstName = rdr["firstName"].ToString(),
                        lastName = rdr["lastName"].ToString(),
                        email = rdr["email"].ToString(),
                        passwordUserd = rdr["passwordUserd"].ToString()
                    };

                    parents.Add(parent);
                }

                con.Close();
            }

            return parents;
        }

        public static Parent GetParentByEmail(string email)
        {
            Parent parent = null;
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Parents WHERE email = @email", con);

                cmd.Parameters.AddWithValue("@email", email);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    parent = new Parent
                    {
                        idParent = rdr["idParent"].ToString(),
                        firstName = rdr["firstName"].ToString(),
                        lastName = rdr["lastName"].ToString(),
                        email = rdr["email"].ToString(),
                        passwordUserd = rdr["passwordUserd"].ToString()
                    };
                }
                con.Close();
            }

            return parent;
        }
        public static Parent GetParentById(string idParent)
        {
            Parent parent = null;
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Parents WHERE idParent = @idParent", con);

                cmd.Parameters.AddWithValue("@idParent", idParent);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    parent = new Parent
                    {
                        idParent = rdr["idParent"].ToString(),
                        firstName = rdr["firstName"].ToString(),
                        lastName = rdr["lastName"].ToString(),
                        email = rdr["email"].ToString(),
                        passwordUserd = rdr["passwordUserd"].ToString()
                    };
                }
                con.Close();
            }

            return parent;
        }

        public static bool AddChild(Baby baby)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Children (idChild, idParent, childName, birthdate) VALUES (@idChild, @idParent, @childName, @birthdate)", con);

                    cmd.Parameters.AddWithValue("@idChild", baby.idChild);
                    cmd.Parameters.AddWithValue("@idParent", baby.parentId); 
                    cmd.Parameters.AddWithValue("@childName", baby.childName);
                    cmd.Parameters.AddWithValue("@birthdate", baby.birthdate);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }


        public static bool EditChild(Baby baby)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Children SET childName = @childName, birthdate = @birthdate WHERE idChild = @idChild", con);

                cmd.Parameters.AddWithValue("@childName", baby.childName);
                cmd.Parameters.AddWithValue("@birthdate", baby.birthdate);
                cmd.Parameters.AddWithValue("@idChild", baby.idChild);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0;
            }
        }

        public static bool DeleteChild(string idChild)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Children WHERE idChild = @idChild", con);

                cmd.Parameters.AddWithValue("@idChild", idChild);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0;
            }
        }

        public static List<Baby> GetAllChildren()
        {
            List<Baby> children = new List<Baby>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT c.idChild, c.childName, c.birthdate, p.idParent " +
                    "FROM Children c " +
                    "INNER JOIN Parents p ON c.idParent = p.idParent", con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Baby baby = new Baby
                    {
                        idChild = rdr["idChild"].ToString(),
                        childName = rdr["childName"].ToString(),
                        birthdate = Convert.ToDateTime(rdr["birthdate"]),
                        parentId = rdr["idParent"].ToString()
                    };

                    children.Add(baby);
                }

                con.Close();
            }

            return children;
        }

        public static Baby GetBabyById(string idChild)
        {
            Baby baby = null;
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Children WHERE idChild = @idChild", con);

                cmd.Parameters.AddWithValue("@idChild", idChild);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    baby = new Baby
                    {
                        idChild = rdr["idChild"].ToString(),
                        childName = rdr["childName"].ToString(),
                        birthdate = Convert.ToDateTime(rdr["birthdate"])
                    };
                }
                con.Close();
            }

            return baby;
        }
        public static List<Baby> GetChildrenByParentId(string parentId)
        {
            List<Baby> children = new List<Baby>();

            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT c.idChild, c.childName, c.birthdate " +
                "FROM Children c " +
                "INNER JOIN Parents p ON c.idParent = p.idParent " +
                "WHERE p.idParent = @idParent", con))
            {
                cmd.Parameters.Add("@idParent", SqlDbType.NVarChar).Value = parentId;

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        Baby baby = new Baby
                        {
                            idChild = rdr["idChild"].ToString(),
                            childName = rdr["childName"].ToString(),
                            birthdate = Convert.ToDateTime(rdr["birthdate"])
                        };
                        children.Add(baby);
                    }
                }
            }

            return children;
        }

        public static bool AddFeed(Feed feed)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand checkCmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Feed WHERE idChild = @idChild AND timeDate = @timeDate", con);
                checkCmd.Parameters.AddWithValue("@idChild", feed.idBaby);
                checkCmd.Parameters.AddWithValue("@timeDate", feed.FeedTime);

                con.Open();
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    SqlCommand updateCmd = new SqlCommand(
                        "UPDATE Feed SET amount = @amount WHERE idChild = @idChild AND timeDate = @timeDate", con);
                    updateCmd.Parameters.AddWithValue("@idChild", feed.idBaby);
                    updateCmd.Parameters.AddWithValue("@timeDate", feed.FeedTime);
                    updateCmd.Parameters.AddWithValue("@amount", feed.amount);

                    int rowsAffected = updateCmd.ExecuteNonQuery();
                    con.Close();

                    return rowsAffected > 0;
                }
                else
                {
                    SqlCommand insertCmd = new SqlCommand(
                        "INSERT INTO Feed (idChild, timeDate, amount) VALUES (@idChild, @timeDate, @amount)", con);
                    insertCmd.Parameters.AddWithValue("@idChild", feed.idBaby);
                    insertCmd.Parameters.AddWithValue("@timeDate", feed.FeedTime);
                    insertCmd.Parameters.AddWithValue("@amount", feed.amount);

                    int rowsAffected = insertCmd.ExecuteNonQuery();
                    con.Close();

                    return rowsAffected > 0;
                }
            }
        }


        public static bool EditFeed(Feed feed)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Feed SET amount = @amount WHERE idChild = @idChild AND timeDate = @timeDate", con);

                cmd.Parameters.AddWithValue("@idChild", feed.idBaby);
                cmd.Parameters.AddWithValue("@timeDate", feed.FeedTime);
                cmd.Parameters.AddWithValue("@amount", feed.amount);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0;
            }
        }


        public static bool DeleteFeed(string idChild, DateTime timeDate)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Feed WHERE idChild = @idChild AND timeDate = @timeDate", con);

                cmd.Parameters.AddWithValue("@idChild", idChild);
                cmd.Parameters.AddWithValue("@timeDate", timeDate);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0;
            }
        }

        public static List<Feed> GetAllFeeds()
        {
            List<Feed> feeds = new List<Feed>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Feed", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Feed feed = new Feed
                    {
                        idBaby = rdr["idChild"].ToString(),
                        FeedTime = Convert.ToDateTime(rdr["timeDate"]),
                        amount = Convert.ToInt32(rdr["amount"])
                    };

                    feeds.Add(feed);
                }

                con.Close();
            }

            return feeds;
        }

        public static Feed GetFeedById(string idChild, DateTime timeDate)
        {
            Feed feed = null;
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Feed WHERE idChild = @idChild AND timeDate = @timeDate", con);

                cmd.Parameters.AddWithValue("@idChild", idChild);
                cmd.Parameters.AddWithValue("@timeDate", timeDate);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    feed = new Feed
                    {
                        idBaby = rdr["idChild"].ToString(),
                        FeedTime = Convert.ToDateTime(rdr["timeDate"]),
                        amount = Convert.ToInt32(rdr["amount"])
                    };
                }
                con.Close();
            }

            return feed;
        }


        public static bool AddSleep(Sleep sleep)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Sleep (timeDateStart, timeDateEnd, idChild, totalHours) VALUES (@timeDateStart, @timeDateEnd, @idChild, @totalHours)", con);

                cmd.Parameters.AddWithValue("@timeDateStart", sleep.timeDateStart);
                cmd.Parameters.AddWithValue("@timeDateEnd", sleep.timeDateEnd);
                cmd.Parameters.AddWithValue("@idChild", sleep.idChild);
                cmd.Parameters.AddWithValue("@totalHours", sleep.totalHours);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0;
            }
        }

        public static bool EditSleep(Sleep sleep)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Sleep SET timeDateEnd = @timeDateEnd, totalHours = @totalHours WHERE idChild = @idChild AND timeDateStart = @timeDateStart", con);

                cmd.Parameters.AddWithValue("@timeDateStart", sleep.timeDateStart);
                cmd.Parameters.AddWithValue("@timeDateEnd", sleep.timeDateEnd);
                cmd.Parameters.AddWithValue("@idChild", sleep.idChild);
                cmd.Parameters.AddWithValue("@totalHours", sleep.totalHours);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0;
            }
        }

        public static bool DeleteSleep(string idChild, DateTime timeDateStart)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Sleep WHERE idChild = @idChild AND timeDateStart = @timeDateStart", con);

                cmd.Parameters.AddWithValue("@idChild", idChild);
                cmd.Parameters.AddWithValue("@timeDateStart", timeDateStart);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0;
            }
        }

        public static List<Sleep> GetAllSleepRecords()
        {
            List<Sleep> sleeps = new List<Sleep>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Sleep", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Sleep sleep = new Sleep
                    {
                        timeDateStart = Convert.ToDateTime(rdr["timeDateStart"]),
                        timeDateEnd = Convert.ToDateTime(rdr["timeDateEnd"]),
                        idChild = rdr["idChild"].ToString(),
                        totalHours = Convert.ToInt32(rdr["totalHours"])
                    };

                    sleeps.Add(sleep);
                }

                con.Close();
            }

            return sleeps;
        }

        public static Sleep GetSleepById(string idChild, DateTime timeDateStart)
        {
            Sleep sleep = null;
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Sleep WHERE idChild = @idChild AND timeDateStart = @timeDateStart", con);

                cmd.Parameters.AddWithValue("@idChild", idChild);
                cmd.Parameters.AddWithValue("@timeDateStart", timeDateStart);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    sleep = new Sleep
                    {
                        timeDateStart = Convert.ToDateTime(rdr["timeDateStart"]),
                        timeDateEnd = Convert.ToDateTime(rdr["timeDateEnd"]),
                        idChild = rdr["idChild"].ToString(),
                        totalHours = Convert.ToInt32(rdr["totalHours"])
                    };
                }
                con.Close();
            }

            return sleep;
        }
        public static bool AddVaccination(Vaccination vaccination)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string query = @"
                    INSERT INTO Vaccination (idChild, timeDate, TypeVaccination, nodes)
                    VALUES (@idChild, @timeDate, @TypeVaccination, @nodes)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@idChild", vaccination.idChild);
                        cmd.Parameters.AddWithValue("@timeDate", vaccination.timeDate);
                        cmd.Parameters.AddWithValue("@TypeVaccination", vaccination.typeVaccination);
                        cmd.Parameters.AddWithValue("@nodes", vaccination.nodes);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"SQL error occurred while adding vaccination: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error occurred while adding vaccination: {ex.Message}");
                return false;
            }
        }

        public static bool EditVaccination(Vaccination vaccination)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string query = @"
                MERGE INTO Vaccination AS target
                USING (SELECT @idChild AS idChild, @timeDate AS timeDate, @TypeVaccination AS TypeVaccination, @nodes AS nodes) AS source
                ON (target.idChild = source.idChild AND target.timeDate = source.timeDate AND target.TypeVaccination = source.TypeVaccination)
                WHEN MATCHED THEN
                    UPDATE SET
                        nodes = source.nodes,
                        timeDate = source.timeDate
                WHEN NOT MATCHED THEN
                    INSERT (idChild, timeDate, TypeVaccination, nodes)
                    VALUES (source.idChild, source.timeDate, source.TypeVaccination, source.nodes);
                ";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.Add("@idChild", SqlDbType.NVarChar).Value = vaccination.idChild;
                        cmd.Parameters.Add("@timeDate", SqlDbType.DateTime).Value = vaccination.timeDate;
                        cmd.Parameters.Add("@TypeVaccination", SqlDbType.Int).Value = vaccination.typeVaccination;
                        cmd.Parameters.Add("@nodes", SqlDbType.NVarChar).Value = vaccination.nodes;

                        Debug.WriteLine($"Attempting to update or insert vaccination: idChild={vaccination.idChild}, timeDate={vaccination.timeDate}, TypeVaccination={vaccination.typeVaccination}, nodes={vaccination.nodes}");

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        Debug.WriteLine($"Rows affected: {rowsAffected}");

                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"SQL error occurred while updating or inserting vaccination: {ex.Message}");
                Debug.WriteLine($"State: {ex.State}");
                Debug.WriteLine($"LineNumber: {ex.LineNumber}");
                foreach (SqlError sqlErr in ex.Errors)
                {
                    Debug.WriteLine($"Message: {sqlErr.Message} | Number: {sqlErr.Number}");
                }
                throw; // Re-throw the exception for proper error handling
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unexpected error occurred while updating or inserting vaccination: {ex.Message}");
                Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw; // Re-throw the exception for proper error handling
            }
        }



        public static bool DeleteVaccination(string idChild, DateTime timeDate)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Vaccination WHERE idChild = @idChild AND timeDate = @timeDate", con);

                cmd.Parameters.AddWithValue("@idChild", idChild);
                cmd.Parameters.AddWithValue("@timeDate", timeDate);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0;
            }
        }

        public static List<Vaccination> GetAllVaccinations()
        {
            List<Vaccination> vaccinations = new List<Vaccination>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = @"
                    SELECT 
                        TypeVaccination.descreption,
                        Vaccination.idChild,
                        Vaccination.timeDate,
                        Vaccination.nodes,
                        Vaccination.TypeVaccination
                    FROM 
                        dbo.TypeVaccination
                    INNER JOIN 
                        dbo.Vaccination 
                    ON 
                        dbo.TypeVaccination.typeVaccination = dbo.Vaccination.TypeVaccination";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Vaccination vaccination = new Vaccination
                    {
                        idChild = rdr["idChild"].ToString(),
                        timeDate = Convert.ToDateTime(rdr["timeDate"]),
                        typeVaccination = Convert.ToInt32(rdr["TypeVaccination"]),
                        nodes = rdr["nodes"].ToString(),
                        descreption = rdr["descreption"].ToString()
                    };

                    vaccinations.Add(vaccination);
                }

                con.Close();
            }

            return vaccinations;
        }

        public static Vaccination GetVaccinationById(string idChild, DateTime timeDate)
        {
            Vaccination vaccination = null;

            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = @"
                    SELECT 
                        TypeVaccination.descreption,
                        Vaccination.idChild,
                        Vaccination.timeDate,
                        Vaccination.nodes,
                        Vaccination.TypeVaccination
                    FROM 
                        dbo.TypeVaccination
                    INNER JOIN 
                        dbo.Vaccination 
                    ON 
                        dbo.TypeVaccination.typeVaccination = dbo.Vaccination.TypeVaccination
                    WHERE 
                        Vaccination.idChild = @idChild AND Vaccination.timeDate = @timeDate";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idChild", idChild);
                cmd.Parameters.AddWithValue("@timeDate", timeDate);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    vaccination = new Vaccination
                    {
                        idChild = rdr["idChild"].ToString(),
                        timeDate = Convert.ToDateTime(rdr["timeDate"]),
                        typeVaccination = Convert.ToInt32(rdr["TypeVaccination"]),
                        nodes = rdr["nodes"].ToString(),
                        descreption = rdr["descreption"].ToString()
                    };
                }

                con.Close();
            }

            return vaccination;
        }


        public static bool AddGrowth(Growth growth)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string query = @"
                    INSERT INTO Growth (idChild, timeDate, typeGrowth, descreption)
                    VALUES (@idChild, @timeDate, @typeGrowth, @descreption)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@idChild", growth.idChild);
                        cmd.Parameters.AddWithValue("@timeDate", growth.timeDate);
                        cmd.Parameters.AddWithValue("@typeGrowth", growth.typeGrowth);
                        cmd.Parameters.AddWithValue("@descreption", growth.descreption);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"SQL error occurred while adding growth: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error occurred while adding growth: {ex.Message}");
                return false;
            }
        }

        public static bool EditGrowth(Growth growth)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string query = @"
                    MERGE INTO Growth AS target
                    USING (SELECT @idChild AS idChild, @timeDate AS timeDate, @typeGrowth AS typeGrowth, @descreption AS descreption) AS source
                    ON (target.idChild = source.idChild AND target.timeDate = source.timeDate AND target.typeGrowth = source.typeGrowth)
                    WHEN MATCHED THEN
                        UPDATE SET
                            descreption = source.descreption
                    WHEN NOT MATCHED THEN
                        INSERT (idChild, timeDate, typeGrowth, descreption)
                        VALUES (source.idChild, source.timeDate, source.typeGrowth, source.descreption);
                    ";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@idChild", SqlDbType.NVarChar).Value = growth.idChild;
                        cmd.Parameters.Add("@timeDate", SqlDbType.DateTime).Value = growth.timeDate;
                        cmd.Parameters.Add("@typeGrowth", SqlDbType.Int).Value = growth.typeGrowth;
                        cmd.Parameters.Add("@descreption", SqlDbType.NVarChar).Value = growth.descreption;

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"SQL error occurred while updating or inserting growth: {ex.Message}");
                Debug.WriteLine($"State: {ex.State}");
                Debug.WriteLine($"LineNumber: {ex.LineNumber}");
                foreach (SqlError sqlErr in ex.Errors)
                {
                    Debug.WriteLine($"Message: {sqlErr.Message} | Number: {sqlErr.Number}");
                }
                throw; 
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unexpected error occurred while updating or inserting growth: {ex.Message}");
                Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw; 
            }
        }

        public static bool DeleteGrowth(string idChild, DateTime timeDate, int typeGrowth)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string query = @"
                    DELETE FROM Growth
                    WHERE idChild = @idChild AND timeDate = @timeDate AND typeGrowth = @typeGrowth";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@idChild", idChild);
                        cmd.Parameters.AddWithValue("@timeDate", timeDate);
                        cmd.Parameters.AddWithValue("@typeGrowth", typeGrowth);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"SQL error occurred while deleting growth: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error occurred while deleting growth: {ex.Message}");
                return false;
            }
        }

        public static List<Growth> GetAllGrowths()
        {
            List<Growth> growths = new List<Growth>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = @"
                SELECT 
                    idChild,
                    timeDate,
                    typeGrowth,
                    descreption
                FROM 
                    Growth";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Growth growth = new Growth
                        {
                            idChild = rdr["idChild"].ToString(),
                            timeDate = Convert.ToDateTime(rdr["timeDate"]),
                            typeGrowth = Convert.ToInt32(rdr["typeGrowth"]),
                            descreption = rdr["descreption"].ToString()
                        };

                        growths.Add(growth);
                    }

                    con.Close();
                }
            }

            return growths;
        }

        public static Growth GetGrowthById(string idChild, DateTime timeDate, int typeGrowth)
        {
            Growth growth = null;

            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = @"
                SELECT 
                    idChild,
                    timeDate,
                    typeGrowth,
                    descreption
                FROM 
                    Growth
                WHERE 
                    idChild = @idChild AND timeDate = @timeDate AND typeGrowth = @typeGrowth";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idChild", idChild);
                    cmd.Parameters.AddWithValue("@timeDate", timeDate);
                    cmd.Parameters.AddWithValue("@typeGrowth", typeGrowth);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        growth = new Growth
                        {
                            idChild = rdr["idChild"].ToString(),
                            timeDate = Convert.ToDateTime(rdr["timeDate"]),
                            typeGrowth = Convert.ToInt32(rdr["typeGrowth"]),
                            descreption = rdr["descreption"].ToString()
                        };
                    }

                    con.Close();
                }
            }

            return growth;
        }
        public static bool AddTypeVaccination(TypeVaccination typeVaccination)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string query = @"
                    INSERT INTO TypeVaccination (typeVaccination, descreption)
                    VALUES (@TypeVaccinationId, @Description)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@TypeVaccinationId", typeVaccination.TypeVaccinationId);
                        cmd.Parameters.AddWithValue("@Description", typeVaccination.Description);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"SQL error occurred while adding type vaccination: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error occurred while adding type vaccination: {ex.Message}");
                return false;
            }
        }

        public static TypeVaccination GetTypeVaccinationById(int id)
        {
            TypeVaccination typeVaccination = null;

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string query = @"
                    SELECT typeVaccination, descreption
                    FROM TypeVaccination
                    WHERE typeVaccination = @TypeVaccinationId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@TypeVaccinationId", id);

                        con.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                typeVaccination = new TypeVaccination
                                {
                                    TypeVaccinationId = Convert.ToInt32(rdr["typeVaccination"]),
                                    Description = rdr["descreption"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"SQL error occurred while retrieving type vaccination: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error occurred while retrieving type vaccination: {ex.Message}");
            }

            return typeVaccination;
        }

        public static bool EditTypeVaccination(TypeVaccination typeVaccination)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string query = @"
                    MERGE INTO TypeVaccination AS target
                    USING (SELECT @TypeVaccinationId AS typeVaccination, @Description AS descreption) AS source
                    ON (target.typeVaccination = source.typeVaccination)
                    WHEN MATCHED THEN
                        UPDATE SET descreption = source.descreption
                    WHEN NOT MATCHED THEN
                        INSERT (typeVaccination, descreption)
                        VALUES (source.typeVaccination, source.descreption);";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@TypeVaccinationId", typeVaccination.TypeVaccinationId);
                        cmd.Parameters.AddWithValue("@Description", typeVaccination.Description);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"SQL error occurred while updating or inserting type vaccination: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error occurred while updating or inserting type vaccination: {ex.Message}");
                return false;
            }
        }

        public static bool DeleteTypeVaccination(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string query = @"
                    DELETE FROM TypeVaccination
                    WHERE typeVaccination = @TypeVaccinationId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@TypeVaccinationId", id);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"SQL error occurred while deleting type vaccination: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error occurred while deleting type vaccination: {ex.Message}");
                return false;
            }
        }

        public static List<TypeVaccination> GetAllTypeVaccinations()
        {
            List<TypeVaccination> types = new List<TypeVaccination>();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string query = @"
                    SELECT typeVaccination, descreption
                    FROM TypeVaccination";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                types.Add(new TypeVaccination
                                {
                                    TypeVaccinationId = Convert.ToInt32(rdr["typeVaccination"]),
                                    Description = rdr["descreption"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"SQL error occurred while retrieving all type vaccinations: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error occurred while retrieving all type vaccinations: {ex.Message}");
            }

            return types;
        }
    }

}
