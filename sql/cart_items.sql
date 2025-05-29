CREATE TABLE cart_items (
                            user_id INTEGER NOT NULL,
                            product_id INTEGER NOT NULL,
                            quantity INTEGER NOT NULL DEFAULT 1,
                            PRIMARY KEY (user_id, product_id)
);