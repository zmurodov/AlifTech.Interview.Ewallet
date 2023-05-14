create table replenishments
(
    id        bigserial      not null primary key,
    user_id   varchar(255)   not null,
    amount    decimal(10, 2) not null,
    wallet_id varchar(255)   not null,
    date      timestamp without time zone,

    constraint replenishments_wallet_id_fk
        foreign key (wallet_id) references wallets (id) on delete cascade,

    constraint replenishments_user_id_fk
        foreign key (user_id) references users (id) on delete cascade
);