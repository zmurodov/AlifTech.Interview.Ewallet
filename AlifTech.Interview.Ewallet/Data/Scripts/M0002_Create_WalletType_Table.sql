create table wallet_types
(
    id          int            not null primary key,
    name        varchar(255)   null,
    max_balance decimal(10, 2) null,

    constraint wallet_types_name_uindex
        unique (name)
)