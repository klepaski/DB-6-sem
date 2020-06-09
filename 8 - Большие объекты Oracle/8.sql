/*
BLOB - неструкт двоич д-е               хран в БД (внут)
CLOB - символьные д-е                   хран в БД (внут)
BILE - внеш файлы ОС, сод. двоич д-е    вне БД (внеш)

LOB сост из локатора (внут ук на знач) + значение (реальное содержимое)
*/

-- созд отд папку для WORD (PDF)
create directory bfile_dir as 'D:\pict\';


-- доб в табл FOTO BLOB, DOC BFILE
create table BigFiles (
    username varchar2(50) not null,
    foto blob not null,
    doc bfile
    );


-- доб фотки и доки в таблицу
insert into BigFiles
    values ('Julia',
            utl_raw.cast_to_raw('D:\pict\mysh.png'),
            BFILENAME( 'bfile_dir', 'db.docx'));


select * from BigFiles;





