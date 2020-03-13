IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Customers] (
    [CustomerId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([CustomerId])
);

GO

CREATE TABLE [Products] (
    [ProductId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Price] int NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([ProductId])
);

GO

CREATE TABLE [Orders] (
    [OrderId] int NOT NULL IDENTITY,
    [CustomerId] int NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([OrderId]),
    CONSTRAINT [FK_Orders_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([CustomerId]) ON DELETE CASCADE
);

GO

CREATE TABLE [ProductInOrders] (
    [ProductInOrderId] int NOT NULL IDENTITY,
    [Quantity] int NOT NULL,
    [Paid] bit NOT NULL,
    [ProductId] int NOT NULL,
    [OrderId] int NOT NULL,
    CONSTRAINT [PK_ProductInOrders] PRIMARY KEY ([ProductInOrderId]),
    CONSTRAINT [FK_ProductInOrders_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([OrderId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductInOrders_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Orders_CustomerId] ON [Orders] ([CustomerId]);

GO

CREATE INDEX [IX_ProductInOrders_OrderId] ON [ProductInOrders] ([OrderId]);

GO

CREATE INDEX [IX_ProductInOrders_ProductId] ON [ProductInOrders] ([ProductId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200313195333_Initial', N'3.1.1');

GO