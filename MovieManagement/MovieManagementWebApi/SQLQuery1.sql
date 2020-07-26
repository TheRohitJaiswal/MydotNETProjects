select * from Users
select * from genres
select * from MovieItems
select * from FavoriteMovies
select * from Favorites


sp_help FavoriteMovies
sp_help MovieItems
sp_help Users


insert into FavoriteMovies
values (7,2)

insert into Users
values('Anupam','anupam',3,1)

delete from FavoriteMovies where FavoriteId = 5
delete from MovieItems
drop table MovieItems
delete from Favorites
drop table Favorites
delete from Genres
drop table Genres
delete from Users where Id>1
drop table Users
delete from FavoriteMovies
drop table FavoriteMovies