using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Kolokwium.DTOs;
using Kolokwium.Models;

namespace Kolokwium.Service
{
    public class DbService : IDbService
    {
        public string connectionS = "Data Source=db-mssql;Initial Catalog=s19562;Integrated Security=True";

        public string AddTask(AddTaskRequest request)
        {
            using (var con = new SqlConnection(connectionS))
            using (var com = new SqlCommand())
            {

                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();
                com.Transaction = tran;

             
                com.CommandText = "SET IDENTITY_INSERT Task ON INSERT INTO Task( Name, Descripton, Deadline, IdProject ,IdTaskType, IdAssignedTo, IdCreator)" +
                    "VALUES(@IdAnimal, @Name, @Type, @AdmissionDate , @IdOwner)";
                com.Parameters.AddWithValue("Name", request.Name);
                com.Parameters.AddWithValue("Description", request.Name);
                com.Parameters.AddWithValue("Deadline", request.DeadLine);
                com.Parameters.AddWithValue("IdProject", request.IdProject);
                com.Parameters.AddWithValue("IdTaskType", request.IdTaskType);
                com.Parameters.AddWithValue("IdAssignedTo", request.IdAssignedTo);
                com.Parameters.AddWithValue("IdCreatore", request.IdCreator);

                Console.WriteLine("Dodano taska do bazy"); //why to sie nie wyswitle ?

                com.ExecuteNonQuery();
                //tran.Commit();

                com.CommandText = "select IdTaskType from TaskType where IdTaskType=@IdTaskType";
                com.Parameters.AddWithValue("IdTaskType", request.IdTaskType);

                var dr = com.ExecuteReader();

                if (!dr.Read())
                {
                    com.CommandText = "INSERT INTO TaskType(IdTaskType, Name) VALUES(@IdTaskType, @Name)";
                    com.Parameters.AddWithValue("IdTaskTypee", request.IdTaskType);
                    com.Parameters.AddWithValue("Name", request.Name);
              
                    dr.Close();
                    com.ExecuteNonQuery();
                }
                else
                {
                  
                    dr.Close();
                    //tran.Rollback();


                }


                tran.Commit();
                con.Close(); //potrzbne ?
                return ("sprawdz");
            }

        }

        public IEnumerable<Taski> GetTasks(int id)
        {
            var taski = new List<Taski>();

            using (var con = new SqlConnection(connectionS))
            using (var com = new SqlCommand())
            {

               
                   
               

                com.Connection = con;
                com.CommandText = "select * from Task t join Project p on " +
                    " t.IdProject = p.IdProject WHERE p.IdProject ="+id + " order by t.Deadline DESC";

                //select * from Task t join Project p on t.IdProject = p.IdProject WHERE p.IdProject = 1 order by t.Deadline DESC; DZIALA

                con.Open();
                var dr = com.ExecuteReader();


                while (dr.Read())
                {
                    var t = new Taski();

                    t.IdTask = (int)dr["IdTask"];
                    t.Name = dr["Name"].ToString();
                    t.Description = dr["Description"].ToString();
                    t.DeadLine = (DateTime)dr["Deadline"];
                    t.IdProject = (int)dr["IdProject"];
                    t.IdTaskType = (int)dr["IdTaskType"];
                    

                    Console.WriteLine(taski);
                    taski.Add(t);
                }
                return taski;
            }
        


        }





    }
}
