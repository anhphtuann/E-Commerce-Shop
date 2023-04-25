drop database Shop

create database Shop

create table Products(
id int identity,
productName nvarchar(50) unique,
productDescription nvarchar(max),
primary key (Id)
)

create table Options(
id int,
productId int,
optionName nvarchar(50),
primary key (id, productId),
foreign key (productId) references Products(Id)
)

create table OptionValues(
id int,
productId int,
optionId int,
valueName nvarchar(50),
primary key (id, productId, optionId),
foreign key (optionId, productId) references Options(id, productId)
)

create table ProductSKUs(
id int identity,
productId int,
sku nvarchar(50),
price money,
primary key (id, productId),
foreign key(productId) references Products(id)
)

create table SKUValues(
productId int,
optionId int,
skuId int,
valueId int,
primary key(productId, optionId, skuId),
foreign key(optionId, productId) references Options(id, productId),
foreign key(skuId, productId) references ProductSKUs(id, productId),
foreign key(valueId, productId, optionId) references OptionValues(id, productId, optionId),
)


select * from Products

drop table ProductSKUs
drop table Products
drop table OptionValues
drop table Options
drop table SKUValues

use Shop

ALTER TABLE Options
DROP CONSTRAINT UQ__Options__099DDDA87C2AF231;
