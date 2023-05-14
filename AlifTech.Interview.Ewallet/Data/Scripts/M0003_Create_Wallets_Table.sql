create table wallets
(
    id             varchar(255)   not null primary key,
    user_id        varchar(255)   not null,
    balance        decimal(10, 2) not null,
    wallet_type_id int            not null,

    constraint fk_wallets_users
        foreign key (user_id) references users (id),

    constraint fk_wallets_wallet_types
        foreign key (wallet_type_id) references wallet_types (id)
);