CREATE TABLE projects
(
    id                SERIAL PRIMARY KEY,
    title             VARCHAR(100)   NOT NULL,
    project_code      CHAR(6)        NOT NULL,
    description       VARCHAR(500),
    start_date        DATE           NOT NULL,
    expected_end_date DATE,
    priority          INTEGER        NOT NULL,
    budget            DECIMAL(12, 2) NOT NULL
);

CREATE TABLE products
(
    id                SERIAL PRIMARY KEY,
    name             VARCHAR(100)   NOT NULL,
    description       VARCHAR(500),
    type          VARCHAR(100)        NOT NULL,
    prix            DECIMAL(12, 2) NOT NULL
);