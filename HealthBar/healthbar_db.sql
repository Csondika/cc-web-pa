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

INSERT INTO users(name, password, email, role) VALUES ('homeless', '982d9e3eb996f559e633f4d194def3761d909f5a3b647d1a851fead67c32c9d1', 'homeless@mail.com', 'user');
INSERT INTO users(name, password, email, role, city, address, postal_code) VALUES ('test', '982d9e3eb996f559e633f4d194def3761d909f5a3b647d1a851fead67c32c9d1', 'test@mail.com', 'user', 'Kazincbarcika', 'Mátyás Király u. 11', 3700);
INSERT INTO users(name, password, email, role) VALUES ('admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 'admin@mail.com', 'admin');
