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

ALTER TABLE [ProductInOrders] DROP CONSTRAINT [PK_ProductInOrders];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductInOrders]') AND [c].[name] = N'ProductInOrderId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ProductInOrders] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [ProductInOrders] DROP COLUMN [ProductInOrderId];

GO

ALTER TABLE [ProductInOrders] ADD [ProductOrderId] int NOT NULL IDENTITY;

GO

ALTER TABLE [ProductInOrders] ADD CONSTRAINT [PK_ProductInOrders] PRIMARY KEY ([ProductOrderId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200313211806_second', N'3.1.1');

GO

ALTER TABLE [ProductInOrders] DROP CONSTRAINT [FK_ProductInOrders_Orders_OrderId];

GO

ALTER TABLE [ProductInOrders] DROP CONSTRAINT [FK_ProductInOrders_Products_ProductId];

GO

ALTER TABLE [ProductInOrders] DROP CONSTRAINT [PK_ProductInOrders];

GO

EXEC sp_rename N'[ProductInOrders]', N'ProductOrders';

GO

EXEC sp_rename N'[ProductOrders].[IX_ProductInOrders_ProductId]', N'IX_ProductOrders_ProductId', N'INDEX';

GO

EXEC sp_rename N'[ProductOrders].[IX_ProductInOrders_OrderId]', N'IX_ProductOrders_OrderId', N'INDEX';

GO

ALTER TABLE [ProductOrders] ADD CONSTRAINT [PK_ProductOrders] PRIMARY KEY ([ProductOrderId]);

GO

ALTER TABLE [ProductOrders] ADD CONSTRAINT [FK_ProductOrders_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([OrderId]) ON DELETE CASCADE;

GO

ALTER TABLE [ProductOrders] ADD CONSTRAINT [FK_ProductOrders_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200313212621_third', N'3.1.1');

GO
