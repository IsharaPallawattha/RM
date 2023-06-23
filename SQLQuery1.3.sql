create table products
(
pID int primary key identity,
pName varchar(50),
pPrice float,
CategoryID int,
pImage image
)

select pID,pName,pPrice,CategoryID,c.catName from products p inner join category c on c.catID=p.CategoryID 
