using CVLAdminPanel.Helper;
using CVLAdminPanel.Models.Model;
using CVLAdminPanel.Utility;
using CVLAdminPanel.Utility.Database;
using NLog.Web.LayoutRenderers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace CVLAdminPanel.DAL.Repositories
{
    public class PersonalInformationRepository
    {
        private SqlConnection connection;
        private SqlDataReader reader;
        private SqlCommand command;
        public List<PersonalInformation> FindAll()
        {
            List<PersonalInformation> personalInformationList = new List<PersonalInformation>();
            using (connection = Connection.GetConnection())
            {
                using (command = new SqlCommand(Procedures.GetAllPersonalInformation.ToString(), connection))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    using (reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PersonalInformation personalInformation = GetAllPersonalInfomationColumns();
                            personalInformationList.Add(personalInformation);
                        }
                        return personalInformationList;
                    }
                }
            }
        }
        public PersonalInformation FindById(int id)
        {
            PersonalInformation PersonalInformation= null;
            using (connection = Connection.GetConnection())
            {
                using (command = new SqlCommand(Procedures.GetPersonalInformationByLoginId.ToString(), connection))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@LoginId", id));

                    using (reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PersonalInformation = GetAllPersonalInfomationColumns();
                        }
                    }
                }
            }
            return PersonalInformation;
        }

        public bool Save(PersonalInformation PersonalInformation)
        {
            using (connection = Connection.GetConnection())
            {
                using (command = new SqlCommand(Procedures.SavePersonalInformation, connection))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    SetAllParameters(PersonalInformation);
                    return command.ExecuteNonQuery() > 0 ? true : false;
                }
            }
        }

        public bool Update(PersonalInformation PersonalInformation)
        {
            using (connection = Connection.GetConnection())
            {
                using (command = new SqlCommand(Procedures.UpdatePersonalInformation, connection))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    SetAllParameters(PersonalInformation);
                    return command.ExecuteNonQuery() > 0 ? true : false;
                }
            }
        }
        
        public bool Delete(int loginId)
        {
            using (connection = Connection.GetConnection())
            {
                using (command = new SqlCommand(Procedures.DeletePersonalInformation, connection))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@LoginId", loginId));
                    return command.ExecuteNonQuery() > 0 ? true : false;
                }
            }
        }

        ///-------------------PRIVATE METHODS-----------------------///
        private void SetAllParameters(PersonalInformation PersonalInformation)
        {
            command.Parameters.Add(new SqlParameter("@LoginId", PersonalInformation.loginId));
            command.Parameters.Add(new SqlParameter("@FirstName", PersonalInformation.firstName));
            command.Parameters.Add(new SqlParameter("@LastName", PersonalInformation.lastName));
            command.Parameters.Add(new SqlParameter("@Gender", PersonalInformation.gender));
            command.Parameters.Add(new SqlParameter("@DateOfBirth", PersonalInformation.dateOfBirth));
            command.Parameters.Add(new SqlParameter("@Address", PersonalInformation.address));
        }

        private PersonalInformation GetAllPersonalInfomationColumns()
        {
            PersonalInformation personalInformation = new PersonalInformation
            {
                loginId = reader.GetInt32("loginId"),
                firstName = reader.GetString("firstName"),
                lastName = NullAvoider.ReadNullRrString("lastName", reader),
                gender = reader.GetString("gender"),
                dateOfBirth = NullAvoider.ReadNullOrDateTime("dateOfBirth", reader),
                address = NullAvoider.ReadNullRrString("address", reader)
            };
            return personalInformation;
        }
    }
}
