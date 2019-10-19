create table [dbo].[contact] (
	contact_id int identity(1,1) not null,
	[first_name] nvarchar(255) not null,
	[last_name] nvarchar(255) not null,
	constraint [PK_dbo.contact] primary key clustered ([contact_id] asc)
);
create index idx_contact_first_name_last_name on contact (first_name, last_name);

create table [dbo].[entry] (
	entry_id int identity(1,1) not null,
	[descr] nvarchar(20) not null,
	[contact_num] nvarchar(15) not null,
	[contact_id] int not null,
	constraint [PK_dbo.entry] primary key clustered ([entry_id] asc),
	constraint [FK_dbo.entry_dbo.contact] foreign key ([contact_id]) 
		references [dbo].[contact] ([contact_id]) on delete cascade
);
create index idx_entry_contact_id on [entry] (contact_id);