# EncryptedDatabase

This project targets .NET Framework 5.0

1- Create a SQL Database with a table "Customers" in the schema "dbo":
```SQL
CREATE TABLE [dbo].[Customers]( 
[Id] [int] IDENTITY(1,1) NOT NULL, 
[FirstName] [nvarchar](50) NULL, 
[LastName] [nvarchar](50) NULL, 
CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY])
```
2- Create an azure KeyVault with specific rights:
 [Azure KeyVault Rights](https://docs.microsoft.com/en-us/sql/relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted?view=sql-server-ver15#key-vaults)
 
3- We will create the master key and encryption key column. 

4- Right-clic on the specific table and select "Encrypt Columns.."

4.1- On Column Selection, check the specific column you want to Encrypt, for example the FirstName and choose an encryption type.

4.2- On Master Key Configuration, Select Azure KeyVault and log in with your credentials and select the specific subscription and azure keyvault

4.3- On Run Settings, click on Next.

4.4- On Summarize, click on Finish. The Column Master Key and the Column Encryption Key will be created and stored on Azure KeyVault.

5- Create an app registration and generate Client Secret.

6- Go to the specific Azure KeyVault and set an access policy. Use the app registration and select this below rights for KEY PERMISSIONS : GET, UNWRAP KEY, VERIFY. Then Save.

7- Use UserSecret to configure the connectionString and the azure credentials  :
```JSON
{
  "ConnectionStrings": {
    "Default": "Column Encryption Setting=enabled;Server={HostDB};Initial Catalog={DBName};Persist Security Info=False;User ID={User};Password={Password};"
  },
  "ClientId":"{ClientId}",
  "ClientSecret":"{ClientSecret}",
  "TenantId:"{TenantId}"
}
```

NB: Make sure the type of the encrypted column is defined in OnModelCreating method on DbContext
```CSharp
builder.Entity<Customer>(b =>
{
    b.ToTable("Customers").HasKey(s => s.Id);
    b.Property(s => s.FirstName).HasColumnType("nvarchar(50)");
});
```

ENJOY !
