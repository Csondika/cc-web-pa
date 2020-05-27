DROP TABLE IF EXISTS menus_ingredients;
DROP TABLE IF EXISTS menus;
DROP TABLE IF EXISTS ingredients;
DROP TABLE IF EXISTS users;
DROP TYPE IF EXISTS ingredient_type;
DROP TYPE IF EXISTS roles;

CREATE TYPE ingredient_type AS ENUM ('bun', 'meat', 'vegetable', 'side_dish', 'sauce', 'drink');
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
    type ingredient_type  NOT NULL,
	calories REAL NOT NULL,
	unit REAL NOT NULL,
	price REAL NOT NULL,
	is_vegan BOOL NOT NULL
);

CREATE TABLE menus(
    id SERIAL PRIMARY KEY,
    name VARCHAR(50)  NOT NULL,
    price_off INT NOT NULL,
	price INT NOT NULL,
	calories INT NOT NULL,
	is_vegan BOOL NOT NULL,
    user_id INT REFERENCES users(id)
);

CREATE TABLE menus_ingredients(
    menu_id INT REFERENCES menus(id) ON DELETE CASCADE NOT NULL,
    ingredient_id INT REFERENCES ingredients(id) ON DELETE CASCADE NOT NULL,
    quantity INT NOT NULL,
    PRIMARY KEY(menu_id, ingredient_id)
);

INSERT INTO users(name, password, email, role) VALUES ('admin', '5fd924625f6ab16a19cc9807c7c506ae1813490e4ba675f843d5a10e0baacdb8', 'admin@mail.com', 'admin');
INSERT INTO users(name, password, email, role) VALUES ('homeless', '5fd924625f6ab16a19cc9807c7c506ae1813490e4ba675f843d5a10e0baacdb8', 'homeless@mail.com', 'user');
INSERT INTO users(name, password, email, role, city, address, postal_code) VALUES ('test', '5fd924625f6ab16a19cc9807c7c506ae1813490e4ba675f843d5a10e0baacdb8', 'test@mail.com', 'user', 'Kazincbarcika', 'Mátyás Király u. 11', 3700);

INSERT INTO ingredients(name, type, calories, unit, price, is_vegan) VALUES ('Tomato', 'vegetable', 18, 80, 100, 'true');
INSERT INTO ingredients(name, type, calories, unit, price, is_vegan) VALUES ('Salad', 'vegetable', 17, 60, 80, 'true');
INSERT INTO ingredients(name, type, calories, unit, price, is_vegan) VALUES ('Brown Rice Bun', 'bun', 150, 180, 120, 'true');
INSERT INTO ingredients(name, type, calories, unit, price, is_vegan) VALUES ('Whole-Grain Wheat Bun', 'bun', 178, 210, 150, 'true');
INSERT INTO ingredients(name, type, calories, unit, price, is_vegan) VALUES ('Beef Meat', 'meat', 215, 250, 400, 'false');
INSERT INTO ingredients(name, type, calories, unit, price, is_vegan) VALUES ('Chicken Meat', 'meat', 119, 190, 300, 'false');
INSERT INTO ingredients(name, type, calories, unit, price, is_vegan) VALUES ('Pig Meat', 'meat', 156, 320, 175,'false');
INSERT INTO ingredients(name, type, calories, unit, price, is_vegan) VALUES ('Tofu Steak', 'meat', 82, 180, 220,'true');
INSERT INTO ingredients(name, type, calories, unit, price, is_vegan) VALUES ('Onion', 'vegetable', 32, 50, 80, 'true');
INSERT INTO ingredients(name, type, calories, unit, price, is_vegan) VALUES ('Mineral Water', 'drink', 9, 300, 120, 'true');
INSERT INTO ingredients(name, type, calories, unit, price, is_vegan) VALUES ('100% Orange Juice', 'drink', 230, 300, 180, 'true');
INSERT INTO ingredients(name, type, calories, unit, price, is_vegan) VALUES ('Coffee', 'drink', 40, 150, 120, 'true');
INSERT INTO ingredients(name, type, calories, unit, price, is_vegan) VALUES ('Egg', 'meat', 140, 100, 80, 'false');
INSERT INTO ingredients(name, type, calories, unit, price, is_vegan) VALUES ('Low Calory BBQ Sauce', 'sauce', 55, 50, 100, 'true');
INSERT INTO ingredients(name, type, calories, unit, price, is_vegan) VALUES ('Onion Sauce', 'sauce', 76, 50, 100, 'true');
INSERT INTO ingredients(name, type, calories, unit, price, is_vegan) VALUES ('Fit Chili Sauce', 'sauce', 80, 50, 100, 'true');
INSERT INTO ingredients(name, type, calories, unit, price, is_vegan) VALUES ('Organic Ketchup', 'sauce', 90, 50, 100,'true');
INSERT INTO ingredients(name, type, calories, unit, price, is_vegan) VALUES ('Sweet Potato French Fries', 'side_dish', 240, 100, 470,'true');
INSERT INTO ingredients(name, type, calories, unit, price, is_vegan) VALUES ('Caesar Salad', 'side_dish', 120, 100, 300,'true');
INSERT INTO ingredients(name, type, calories, unit, price, is_vegan) VALUES ('Chicken Salad', 'side_dish', 170, 100, 420,'false');

INSERT INTO menus (name, price_off, price, calories, is_vegan, user_id) VALUES ('The Happy Vegan', 20, 832, 627, true, 1);
INSERT INTO menus (name, price_off, price, calories, is_vegan, user_id) VALUES ('Beefy American', 20, 1168, 1895, false, 1);
INSERT INTO menus (name, price_off, price, calories, is_vegan, user_id) VALUES ('Eggcelent Breakfast', 20, 904, 824, false, 1);
INSERT INTO menus (name, price_off, price, calories, is_vegan, user_id) VALUES ('The Healthy Chicken', 20, 1064, 1529, false, 1);

-- The Happy Vegan
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (1, 3, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (1, 8, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (1, 1, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (1, 2, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (1, 15, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (1, 10, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (1, 19, 1);

-- Beefy American
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (2, 4, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (2, 5, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (2, 9, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (2, 2, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (2, 14, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (2, 11, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (2, 18, 1);

-- Eggcelent Breakfast
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (3, 4, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (3, 13, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (3, 1, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (3, 2, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (3, 9, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (3, 16, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (3, 12, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (3, 20, 1);

-- The Healthy Chicken
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (4, 4, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (4, 6, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (4, 1, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (4, 2, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (4, 17, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (4, 11, 1);
INSERT INTO menus_ingredients(menu_id, ingredient_id, quantity) VALUES (4, 20, 1);