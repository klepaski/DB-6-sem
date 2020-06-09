use Sklad
go

select * from Report


--TASK1
-- ���� ������� Report, ���. 2 ������� - id, XML-�������

create table Report (
id INTEGER primary key identity(1,1),
xml_column XML
);




--TASK2
-- ���� ��������� ����� XML, �.��� �-� �� 3 ���� ������, ����.�����, ����� t

drop procedure generateXML

create procedure generateXML
as
declare @x XML
set @x = (Select	name_client [��� �������], 
					name_product [��� ��������], 
					GETDATE() [����]
	from orders ord 
					join products prd on ord.id_product = prd.id_product
					join clients clnt on clnt.id_client = ord.id_client
	FOR XML AUTO);
	SELECT @x
go

execute generateXML;




--TASK3
-- ���� ��������� ������� ����� XML � ������� Report

create procedure InsertInReport
as
DECLARE  @s XML  
SET @s = (Select	name_client [��� �������], 
					name_product [��� ��������], 
					GETDATE() [����]
		from orders ord 
					join products prd on ord.id_product = prd.id_product
					join clients clnt on clnt.id_client = ord.id_client
for xml raw);
--FOR XML AUTO, TYPE);
insert into Report values(@s);
go
  
  execute InsertInReport
  select * from Report;




--TASK4
-- ����. ������ ��� XML-�������� � Report
create primary xml index My_XML_Index on Report(xml_column)

create xml index Second_XML_Index on Report(xml_column)
using xml index My_XML_Index for path




--TASK5
-- ���� ��������� ���� ���� �� XML-������� � Report (�������� - ���� ��������/��)

select * from Report

create procedure SelectData
as
select xml_column.query('/row')
	as[xml_column]
	from Report
	for xml auto, type;
go
execute SelectData



select xml_column.value('(/row/@����)[1]', 'nvarchar(max)')
	as[xml_column]
	from Report
	for xml auto, type;


select r.Id,
	m.c.value('@����', 'nvarchar(max)') as [date]
	from Report as r
	outer apply r.xml_column.nodes('/row') as m(c)



--���������� �/oracle