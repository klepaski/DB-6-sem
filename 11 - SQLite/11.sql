select * from products;
select * from dilers;
select * from clients;
select * from supplies;
select * from orders;

-- Создать представление
create view Brest_client as select * from clients where city_client='Брест';
select * from Brest_client;

-- Создать триггер
create trigger Decrease_kol after insert on orders
    begin
       update products seet kol=kol-NEW.summ;
    end;

select * from products;	-- 6 хурма 740
insert into orders values (5, 35, 1, 1, 6)

delete from orders where id = 5;
update products set kol=740 where id_product=6;