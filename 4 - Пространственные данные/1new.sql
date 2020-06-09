drop table products;
drop table dilers;
drop table clients;
drop table supplies;
drop table orders;

select * from gadm36_blr_1;

create table products (
	id_product int identity (1,1) primary key,
	name_product nvarchar(25),
	kol int check (kol >0)
	);

create table dilers (
	id_diler int identity (1,1) primary key,
	name_diler nvarchar(100),
	city_diler int not null,
	foreign key (city_diler) references gadm36_blr_1(ogr_fid),
	phone nvarchar(25),
	);

create table clients (
	id_client int identity (1,1) primary key,
	name_client nvarchar(100),
	city_client int not null,
	foreign key (city_client) references gadm36_blr_1(ogr_fid)
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
insert into dilers values	('Дербышкин Илья', 2, '375298213235'),
							('Калоша Дарья', 2, '37529821565'), 
							('Чистякова Юлия', 3, '37525623263'),
							('Зака Наталья', 4, '375338218976');
insert into clients values	('Каспер Наталья', 3),--в вершину иерархии
							('Плотников Дмитрий', 4),
							('Медведев Михаил', 4),
							('Личиков Андрей', 2);
insert into supplies values (1, 1, 600), (2, 2, 40), (3, 3, 89), (4, 4, 230);
insert into orders values	(1, 1, 1, 500, '10.07.1998'),
							(2, 2, 2, 23, '08.06.2003'),
							(3, 3, 3, 60, '23.01.2005'),
							(4, 4, 4, 100, '05.12.2019'),
							(1,1,2,10, '10-07-1998'),(1,2,3,400, '10-07-1998'),
						    (2,4,3,45, '10-07-1998'), (3,2,1,765, '10-07-1998'),
						    (2,3,1,12, '10-07-1998');

