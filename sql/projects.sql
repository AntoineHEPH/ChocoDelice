CREATE TABLE products
(
    id                SERIAL PRIMARY KEY,
    name             VARCHAR(100)   NOT NULL,
    description       VARCHAR(500),
    type          VARCHAR(100)        NOT NULL,
    prix            DECIMAL(12, 2) NOT NULL,
    isactive        BOOLEAN NOT NULL DEFAULT TRUE
);