
# origami

Origami is lightweight enterprise application framework in .NET platform. Contains 4 application blocks : Container, Data, Logging, and Security.

Features :

- Simplify manage object construction and its depedency
- Support Depedency Injection and plugable architecture 
- Wrap generic ADO.NET commands
- DBMS agnostic (support SQL Server, Oracle, MySql, etc)
- Implement some patterns for Data Access from Martin Fowler Book (PoEE) such us : repository, data mapper, virtual proxy,fluent interface
- Support fluent interface query
- Support DDD (Domain Driven Design) and PI (Persistence Ignorance)
- Built in Logger available : Console,File, Database, EventLog, Trace, Smtp, and Messaging 
- Authentication & authorization using XML and database
- Cryptography support : symmetric, asymmetric, and hash 

Classic ADO.NET

```
string connStr = @"Data Source=XERIS\SQLEXPRESS;"
      + "Initial Catalog=Northwind;Integrated Security=True";
SqlConnection conn = new SqlConnection(connStr);
conn.Open();

string sql = "SELECT * FROM Customers";
SqlCommand cmd = new SqlCommand(sql, conn);
SqlDataReader rdr = cmd.ExecuteReader();

while (rdr.Read())
{
    Console.WriteLine(rdr["CustomerId"].ToString());
    Console.WriteLine(rdr["CompanyName"].ToString());
}
rdr.Dispose();

```

Origami Way

```
DataSource dataSource = new DataSource();

dataSource.Provider = "System.Data.SqlClient";
dataSource.ConnectionString = @"Data Source=XERIS\SQLEXPRESS;"
        + "Initial Catalog=Northwind;Integrated Security=True";

IDataContext dx = DataContextFactory.CreateInstance(dataSource);

IDataReader rdr = dx.ExecuteReader("SELECT * FROM Customers");
while (rdr.Read())
{
    Console.WriteLine(rdr["CustomerId"].ToString());
    Console.WriteLine(rdr["CompanyName"] ToString());
}

rdr.Dispose();


```

Using Data Mapper

```
IDataContext dx = DataContextFactory.CreateInstance(dataSource);
string sql="SELECT * FROM Customers";
List<Customer> custs = dx.ExecuteList<Customer>(sql, new CustomerMapper());
foreach (Customer cust in custs)
{
    Console.WriteLine(cust.CustomerId);
    Console.WriteLine(cust.CompanyName);
}


public class CustomerMapper : IDataMapper<Customer>
{
     public Customer Map(IDataReader rdr)
     {
          Customer customer = new Customer();            
          customer.CustomerId = rdr["CustomerId"].ToString();
          customer.CompanyName = rdr["CompanyName"].ToString();
          customer.ContactName = rdr["ContactName"].ToString();
            
          return customer;
     }
}


```


