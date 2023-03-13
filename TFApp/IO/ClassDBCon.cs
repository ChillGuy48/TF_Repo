using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace IO
{
    public class ClassDBCon
    {
        private string _connectionString;
        protected SqlConnection con;
        private SqlCommand _command;

        /// <summary>
        /// Default constructor med fast angivelse af connectionstring
        /// </summary>
        public ClassDBCon()
        {
            //_connectionString = @"";
            //con = new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Overloaded constructor hvor connectionstring initialiseres via en overført parameter
        /// </summary>
        /// <param name="inConnectionString">string</param>
        public ClassDBCon(string inConnectionString)
        {
            _connectionString = inConnectionString;
            con = new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Metode der kun er tilgængelig via nedarvning.
        /// Metoden har til formål at gøre det muligt at initialisere connectionstring under
        /// afvikling af programmet og sætte connectionstring efter behov.
        /// </summary>
        /// <param name="inConnectionString">string</param>
        protected void SetCon(string inConnectionString)
        {
            _connectionString = inConnectionString;
            con = new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Åbner op for forbindelsen til datebasen.
        /// Den undersøger om de gælende betingelser er opfyldt for at åbne forbindelsen, inden den åbnes.
        /// Hvis betingelserne ikke er opfyldt, vil den prøve at håndtere de mest almindelige fejl og mangler.
        /// </summary>
        protected void OpenDB()
        {
            try
            {
                if (this.con != null && this.con.State == ConnectionState.Closed)
                {
                    this.con.Open(); // Åbner forbindelsen til DB
                }
                else
                {
                    if (this.con.State == ConnectionState.Open) // Undersøger om fejlen skyldes at der er en åben forbindelse i forvejen
                    {
                        // Hvis ja - lukker de forbindelsen og kalder 'sig selv' igen for at åbne forbindelsen
                        CloseDB();
                        OpenDB(); // Recrusivt kald
                    }
                    else // Hvis det ikke var på grund af en åben forbindelse, må det være på grund af manglende initialisering af 'con'
                    {
                        this.con = new SqlConnection(_connectionString); // Initialisere 'con' med den angivne connectionstring
                        OpenDB(); // Kalder 'sig selv' igen for at åbne forbindelsen
                    }
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Denne metode lukker forbindelsen til DB
        /// </summary>
        protected void CloseDB()
        {
            try
            {
                this.con.Close(); // Lukker forbindelsen
            }
            catch (SqlException ex) // Håndtere de exceptions (fejl) der måtte opstå under kommunikationen med databasen
            {

                throw ex;
            }
        }

        /// <summary>
        /// Denne metode har til formål, at udføre de handlinger i databasen, som ikke kræver at der returneres et resultatsæt.
        /// Metoden vil dog altid returnere en intiger værdi der angiver som handlingen gik godt eller skidt.
        /// Returneres: -1 er handlingen ikke blevet udført
        /// Returneres: Et tal fra 0 til (meget), indikerer at udtrykket kunne eksekveres og angiver hvor mange datasæt der
        /// blev påvirket
        /// </summary>
        /// <param name="sqlQuery">string</param>
        /// <returns>int</returns>
        protected int ExecuteNonQuery(string sqlQuery)
        {
            int res = 0;

            try
            {
                OpenDB(); // Åbner forbindelsen til DB

                // Her initialiseres instansen af SqlCommand med parmeterne string sqlQuery og SqlConnection con
                using (_command = new SqlCommand(sqlQuery, con))
                {
                    res = _command.ExecuteNonQuery(); // Her kaldes databasen og den givne query eksekveres
                }
            }
            catch (SqlException ex) // Håndtere de exceptions (fejl) der måtte opstå under kommunikationen med databasen
            {
                throw ex;
            }
            finally // Ved angivelse 'finally' sikre jeg, at det der står i 'finally' altid bliver udført, uanset om koden kunne eksekveres med eller uden fejl
            {
                CloseDB(); // Lukker forbindelse til DB
            }

            return res;
        }

        /// <summary>
        /// Denne metode håndtere forespørgelser til databasen som skal returnere et resultatsæt.
        /// Det resultatsæt der modtages fra DB, konverteres over i en collection af typen DataTable
        /// </summary>
        /// <param name="sqlQuery">string</param>
        /// <returns>DataTable</returns>
        protected DataTable DBReturnDataTable(string sqlQuery)
        {
            DataTable dtRes = new DataTable();
            try
            {
                OpenDB();
                // Her initaliseres instansen af SqlCommand med parameterne string query og SqlConnection con
                using (_command = new SqlCommand(sqlQuery, con))
                {
                    // Her foretages kaldet til databasen ved, at der oprettes en ny instans af en SqlDataAdapter.
                    using (SqlDataAdapter  adapter = new SqlDataAdapter(_command))
                    {
                        adapter.Fill(dtRes); // Her transformeres data i 'adapter' til formatet DataTable som er
                                             // mere anvendligt i C# koden der skal modtages resultatet af
                                             //forespørgelsen.
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            // Ved angivelse 'finally' sikre jeg, at det der står i 'finally' altid bliver
            // udført, uanset om koden kunne eksekveres med eller uden fejl
            finally
            {
                CloseDB();
            }

            return dtRes;
        }

        /// <summary>
        /// Denne metode skal håndtere forespørgelser til databasen som skal returnere en tekststreng.
        /// </summary>
        /// <param name="sqlQuery">string</param>
        /// <returns>string</returns>
        protected string DBReturnString(string sqlQuery)
        {
            string res = "";
            bool foundData = false;

            try
            {
                OpenDB(); // Åbner forbindelsen til databasen

                // Opretter en ny instans af SqlCommand med parameterene sqlQuery og con,
                // som indeholder henholdvis min sql forspørgelse og information omkring
                // hvilken database data skal hentes fra.
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    // Her eksekveres forespørgelsen på databasen og svaret gemmes i reader som er af datatypen
                    // SqlDataReader der har samme egenskaber som en StreamReader, altså egenskaber der gør den
                    // egnget til at modtage og holde en stream af tekst
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Hvis reader har modtaget et resultat fra databasen, skal den udføre koden i while loopet
                        while (reader.Read())
                        {
                            res = reader.GetString(0); // Læser teksten fra reader og indsætter den i res.
                                                       // Tallet 0 angiver hvorfra i teksten der skal læses, 0 = start
                            foundData = true; // Bolsk værdi, der angiver at der er modtaget et resultat
                        }
                        // Hvis der ikke findes et resultat i databasen, skal der returneres en hjælpetekst
                        if (!foundData)
                        {
                            res = "Der blev ikke findet data";
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                CloseDB();
            }

            return res;
        }

        /// <summary>
        /// Denne metode skal håndtere forespørgelser til databasen som skal returnere et resultatsæt.
        /// Forespørgelsen skal foretage gennem en StoredProcedure på SqlServeren.
        /// Det resultatsæt der modtages fra DB, konverteres over i en collection af typen DataTable
        /// </summary>
        /// <param name="inCommand">SqlCommand</param>
        /// <returns>DataTable</returns>
        protected DataTable MakeCallToStoredProcedure(SqlCommand inCommand)
        {
            DataTable dtRes = new DataTable();

            try
            {
                OpenDB(); // Åbner forbindelse til databasen

                // Her initialisere en instans af SqlDataAdapter med værdien i inCommand
                using (SqlDataAdapter adaoter = new SqlDataAdapter(inCommand))
                {
                    adaoter.Fill(dtRes); // her over føres data fra adapter til den DataTable, metoden skal returnere.
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                CloseDB(); // Lukker forbindelsen til databasen
            }

            return dtRes;
        }
    }
}
