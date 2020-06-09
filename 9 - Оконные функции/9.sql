

select * from orders;


--1.объем заказов опр. поставщика
		select	distinct(dilers.id_diler), dilers.name_diler as Дилер, 
			SUM(orders.summ) OVER (order by name_diler asc)
			as Число_продуктов from orders
			join dilers ON dilers.id_diler = orders.id_diler;

		
--сравн с общим объемом заказов (%)
		with Aggregates as
		(
			select distinct(id_diler), sum(summ) as sumval
			from orders
			group by orders.id_diler
		)
		select distinct (D.name_diler), O.summ,
			cast (100. * O.summ / A.sumval as numeric(5,2)) as pctcust
			from orders as O
			join aggregates as A on O.id_diler = A.id_diler
			join dilers as D on D.id_diler = O.id_diler;



--сравн с наилучшим объемом заказов
		select dilers.name_diler as Имя_поставщика
			, orders.summ as Заработок
			, max(summ) over (partition by orders.id_diler) as Макс_заработок
			, cast(100. * summ / max(summ) over (partition by orders.id_diler)
					as decimal(5,2)) as Процент
			from orders
			join dilers on dilers.id_diler = orders.id_diler;



--3.разбиение результатов запроса на страницы.
--ROW_NUMBER вычисляет уникальные идентификаторы в секции, начиная с 1 и с шагом 1

		select *, row_number() over (partition by id_diler order by id_diler) as rownum
			from orders;


--4. ROW_NUMBER() для удаления дубликатов (в partition надо все поля перечислить)

	select count(*) from orders;
	select * from orders;
	insert into orders values (2,3,1,12, '10-07-1998'), (2,3,1,12, '10-07-1998'), (2,3,1,12, '10-07-1998');


	delete x from (
	  select *, rn=row_number() over (partition by id_diler, id_client, id_product, summ, order_date, order_year order by id_diler)
	  from orders
	) x
	where rn > 1;




--5.вернуть для каждого клиента суммы заказов последних 6 месяцев помесячно

select clients.name_client, orders.summ, order_date
	from orders join clients on clients.id_client = orders.id_client;

select clients.name_client,
	   orders.summ,
	   orders.order_date,
	   sum(summ) over (partition by orders.id_client) as [count],
	   rn = row_number() over (partition by clients.name_client, month(order_date) order by order_date)
	   from orders join clients on clients.id_client = orders.id_client
	   where month(order_date) between month(getdate()) - 6 and month(getdate())
			or month(order_date) between (month(getdate())-6+12) and 12;