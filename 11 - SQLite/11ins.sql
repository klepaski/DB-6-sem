select * from products
select * from dilers
select * from clients
select * from supplies
select * from orders

insert into products values 	(1, 'Молоко', 56445), (2, 'Хлеб', 10000),(3, 'Яблоки', 565656),
			(4, 'Бананы', 8988989), (5, 'Соки', 56), (6, 'Хурма', 28);

insert into dilers values	(1, 'Дербышкин Илья', 'Гомель', '375298213235'),
			(2, 'Калоша Дарья', 'Гродно', '37529821565'), 
			(3, 'Чистякова Юлия', 'Брест', '37525623263'),
			(4, 'Зака Наталья', 'Витебск', '375338218976');

insert into clients values	(1, 'Каспер Наталья', 'Гродно'),
			(2, 'Плотников Дмитрий', 'Брест'),
			(3, 'Медведев Михаил', 'Витебск'),
			(4, 'Личиков Андрей', 'Гомель');

insert into supplies values (1, 600, 1, 1), (2, 40, 2, 2), (3, 89, 3, 3), (4, 230, 4, 4);

insert into orders values	(1, 500, 1, 1, 5),
			(2, 23, 2, 2, 6),						(3, 60, 3, 3, 3),
			(4, 100, 4, 4, 4);