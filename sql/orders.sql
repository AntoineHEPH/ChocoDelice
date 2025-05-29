CREATE TABLE orders (
                        id SERIAL PRIMARY KEY,
                        user_id INT NOT NULL,
                        username VARCHAR(100) NOT NULL,
                        product_id INT NOT NULL,
                        product_name VARCHAR(100) NOT NULL,
                        quantity INT NOT NULL,
                        unit_price DECIMAL(10, 2) NOT NULL,
                        order_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);