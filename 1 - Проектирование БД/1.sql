-- создать бд, таблицы, индексы, представления
-- процедуры, функции, триггеры
-- СКЛАД
create database Sklad;
use Sklad;

drop table products;
drop table dilers;
drop table clients;
drop table supplies;
drop table orders;

create table products (
	id_product int identity (1,1) primary key,
	name_product nvarchar(25),
	kol int check (kol >0)
	);

create table dilers (
	id_diler int identity (1,1) primary key,
	name_diler nvarchar(100),
	city_diler nvarchar(25),
	phone nvarchar(25)
	);

create table clients (
	id_client int identity (1,1) primary key,
	name_client nvarchar(100),
	city_client nvarchar(25)
	--Hid hierarchyid
	);

create table supplies (
	id_supply int identity (1,1) primary key,
	id_diler int not null,
	id_product int not null,

	foreign key (id_diler)  references dilers (id_diler),
	foreign key (id_product)  references products (id_product),
	supp int
	);
	
			
create table orders (
	id_order int identity (1,1) primary key,
	id_diler int not null,
	id_client int not null,
	id_product int not null,

	foreign key (id_diler)  references dilers (id_diler),
	foreign key (id_client)  references clients (id_client),
	foreign key (id_product)  references products (id_product),
	summ int,
	order_date date,
	order_year as year(order_date)
	);



	


------------------------------------------------------------------

insert into products values ('Молоко', 56445), ('Хлеб', 10000), ('Яблоки', 565656),
							('Бананы', 8988989), ('Соки', 56), ('Хурма', 28);
insert into dilers values	('Дербышкин Илья', 'Гомель', '375298213235'),
							('Калоша Дарья', 'Гродно', '37529821565'), 
							('Чистякова Юлия', 'Брест', '37525623263'),
							('Зака Наталья', 'Витебск', '375338218976');
insert into clients values	('Каспер Наталья', 'Гродно'),--в вершину иерархии
							('Плотников Дмитрий', 'Брест'),
							('Медведев Михаил', 'Витебск'),
							('Личиков Андрей', 'Гомель');
insert into supplies values (1, 1, 600), (2, 2, 40), (3, 3, 89), (4, 4, 230);
insert into orders values	(1, 1, 1, 500, '10.07.1998'),
							(2, 2, 2, 23, '08.06.2003'),
							(3, 3, 3, 60, '23.01.2005'),
							(4, 4, 4, 100, '05.12.2019'),
							(1,1,2,10, '10-07-1998'),(1,2,3,400, '10-07-1998'),
						    (2,4,3,45, '10-07-1998'), (3,2,1,765, '10-07-1998'),
						    (2,3,1,12, '10-07-1998');


-------------------------------------------------------------------------------------------
create view Brest_client as select * from clients where city_client='Брест';
select * from Brest_client;

-------------------------------------------------------------------------
create procedure Insert_diler
as
begin try
	insert into dilers values (5, 'Герман Олег', 'Брест', '375298213235')
end try
begin catch
	rollback
	select ERROR_MESSAGE() as ErrorMessage;
end catch
go

exec Insert_diler;
select * from dilers;
delete from dilers where id_diler = 5;

-------------------------------------------------------------------------
create function Select_product (@pid as int)
	returns nvarchar(25)
	begin
		return (select name from products where id_product=@pid);
	end

select dbo.Select_product(1);

-------------------------------------------------------------------------

create trigger Decrease_kol
	on orders
	after insert
	as
	begin try
		update products set kol=kol-summ from INSERTED
		commit;
	end try
	begin catch
		rollback
		select ('Нельзя заказать больше чем есть на складе!') as ErrorMessage;
	end catch
	go		

drop trigger Decrease_kol;

select * from products;
select * from orders;
insert into orders values (1, 1, 5, 100, '10.09.2004');
	--id_diler, id_client, id_product, summ, date)
--create index i1 on orders (id_order ASC, summ DESC);	--некластеризованный




------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------
use Sklad;
drop procedure add_client
drop procedure drop_client;
drop procedure change_client;

CREATE PROCEDURE add_client
	@name nvarchar(100),
	@city nvarchar(25)
AS
	BEGIN
		INSERT INTO clients(name, city) values(@name,@city)
		--DECLARE @userId int;
		SELECT 0;
	END
GO
----
CREATE PROCEDURE drop_client
	@id int
AS
	BEGIN
	DELETE FROM clients where id_client = @id;
		SELECT 0;
	END
GO
----
CREATE PROCEDURE change_client
    @id int,
	@name nvarchar(100),
	@city nvarchar(25)
AS
	BEGIN
	update clients set name=@name, city=@city 
	where id_client=@id;
		SELECT 0;
	END
GO
----
create procedure  getAllClients
AS
select * from clients

-------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------

CREATE PROCEDURE add_product
	@name nvarchar (25),
	@kol int
AS
	BEGIN
		INSERT INTO products (name, kol) values(@name, @kol)
		SELECT 0;
	END
GO
----
CREATE PROCEDURE drop_product
	@id int
AS
	BEGIN
	DELETE FROM products where id_product = @id;
		SELECT 0;
	END
GO
----
CREATE PROCEDURE change_product
    @id int,
	@name nvarchar(25),
	@kol int
AS
	BEGIN
	update products set name=@name, kol=@kol
		where id_product = @id;
		SELECT 0;
	END
GO
----
create procedure  getAllProducts
AS
select * from Products

-------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------

CREATE PROCEDURE add_diler
	@name nvarchar (100),
	@city nvarchar(25),
	@phone nvarchar(25)
AS
	BEGIN
		INSERT INTO dilers(name, city, phone) values (@name, @city, @phone)
		SELECT 0;
	END
GO
----
CREATE PROCEDURE drop_diler
	@id int
AS
	BEGIN
	DELETE FROM dilers where id_diler = @id;
		SELECT 0;
	END
GO
----
CREATE PROCEDURE change_diler
    @id int,
	@name nvarchar(100),
	@city nvarchar(25),
	@phone nvarchar(25)
AS
	BEGIN
	update dilers set name=@name, city=@city, phone=@phone
		where id_diler = @id;
		SELECT 0;
	END
GO
----
create procedure  getAllDilers
AS
select * from dilers;


-------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------

CREATE PROCEDURE add_order
	@id_diler int,
	@id_client int,
	@id_product int,
	@summ int,
	@order_date date
AS
	BEGIN
		INSERT INTO orders values (@id_diler, @id_client, @id_product, @summ, @order_date);
		SELECT 0;
	END
GO
----
CREATE PROCEDURE drop_order
	@id int
AS
	BEGIN
	DELETE FROM orders where id_order = @id;
		SELECT 0;
	END
GO
----
CREATE PROCEDURE change_order
	@id_order int,
	@id_diler int,
	@id_client int,
	@id_product int,
	@summ int,
	@order_date date
AS
	BEGIN
	update orders set id_diler=@id_diler, id_client=@id_client, id_product=@id_product,
		summ=@summ, order_date=@order_date where id_order = @id_order
		SELECT 0;
	END
GO
----
create procedure  getAllOrders
AS
select * from orders


-------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------

CREATE PROCEDURE add_supply
	@id_diler int,
	@id_product int,
	@supp int
AS
	BEGIN
		INSERT INTO supplies values (@id_diler, @id_product, @supp);
		SELECT 0;
	END
GO
----
CREATE PROCEDURE drop_supply
	@id int
AS
	BEGIN
	DELETE FROM supplies where id_supply = @id;
		SELECT 0;
	END
GO
----
CREATE PROCEDURE change_supply
	@id_supply int,
	@id_diler int,
	@id_product int,
	@supp int
AS
	BEGIN
	update supplies set id_diler=@id_diler, id_product=@id_product, supp=@supp
		where id_supply=@id_supply;
		SELECT 0;
	END
GO
----
create procedure  getAllSupplies
AS
select * from supplies













-----------------------

CREATE PROCEDURE spisok_orders
    @datestart date,
	@dateend date
AS
BEGIN
	SELECT clients.name, products.name from ((orders 
		inner join clients on orders.id_client = clients.id_client)
		inner join products on orders.id_product = products.id_product)
		where orders.order_date between @datestart and @dateend
		order by orders.summ asc;
END
GO

