CREATE DATABASE receivables_db;
GO

USE receivables_db;
GO

CREATE TABLE tb_companies (
    Cnpj NVARCHAR(20) NOT NULL PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    MonthlyBilling FLOAT NOT NULL,
    Sector INT NOT NULL
);
GO

CREATE TABLE tb_invoices (
    Number BIGINT NOT NULL PRIMARY KEY,
    Value FLOAT NOT NULL,
    DueDate DATETIME NOT NULL,
    Cnpj NVARCHAR(20) NOT NULL,
    FOREIGN KEY (Cnpj) REFERENCES tb_companies(Cnpj)
);
GO