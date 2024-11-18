.PHONY: all help \
	b build run dev \
	db-up database-up \
	db-int db-interactive \
	db-down database-down

all: run

help:
	@cat Makefile

run:
	@dotnet watch run --project conti_net.csproj

dev:
	@npm run dev

b: build
build:
	@dotnet build

db-up: database-up
database-up:
	@docker-compose up -d

db-int: db-interactive
db-interactive:
	@docker-compose up

db-mig: db-migrate
db-migrate:
	@dotnet ef database update

db-down: database-down
database-down:
	@docker-compose down
