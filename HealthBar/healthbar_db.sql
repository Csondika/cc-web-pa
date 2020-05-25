DROP TABLE IF EXISTS menus_ingredients;
DROP TABLE IF EXISTS menus;
DROP TABLE IF EXISTS ingredients;
DROP TABLE IF EXISTS users;
DROP TYPE IF EXISTS ingredient_type;
DROP TYPE IF EXISTS roles;

CREATE TYPE ingredient_type AS ENUM ('bun', 'meat', 'vegetable', 'side_dish', 'sauce', 'soft_drink');
CREATE TYPE roles AS ENUM ('admin', 'user');

CREATE TABLE users(
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    password VARCHAR(255) NOT NULL,
    email VARCHAR(50) NOT NULL,
    role VARCHAR(6) NOT NULL,
	city VARCHAR(50),
	address VARCHAR(255),
	postal_code INT
);

CREATE TABLE ingredients(
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    type ingredient_type  NOT NULL
);

CREATE TABLE menus(
    id SERIAL PRIMARY KEY,
    name VARCHAR(50)  NOT NULL,
    price_off INT NOT NULL,
    user_id INT REFERENCES users(id)
);

CREATE TABLE menus_ingredients(
    menu_id INT REFERENCES menus(id) ON DELETE CASCADE NOT NULL,
    ingredient_id INT REFERENCES ingredients(id) ON DELETE CASCADE NOT NULL,
    quantity INT NOT NULL,
    PRIMARY KEY(menu_id, ingredient_id)
);

INSERT INTO users(name, password, email, role) VALUES ('homeless', '5fd924625f6ab16a19cc9807c7c506ae1813490e4ba675f843d5a10e0baacdb8', 'homeless@mail.com', 'user');
INSERT INTO users(name, password, email, role, city, address, postal_code) VALUES ('test', '5fd924625f6ab16a19cc9807c7c506ae1813490e4ba675f843d5a10e0baacdb8', 'test@mail.com', 'user', 'Kazincbarcika', 'Mátyás Király u. 11', 3700);
INSERT INTO users(name, password, email, role) VALUES ('admin', '5fd924625f6ab16a19cc9807c7c506ae1813490e4ba675f843d5a10e0baacdb8', 'admin@mail.com', 'admin');
