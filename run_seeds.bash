#!/bin/env bash
# shellcheck disable=1091,2086

set -euo pipefail

. .env

DB_SERVER_URL="${DB_HOST},${DB_PORT}"

docker exec \
    -it mssql-server /opt/mssql-tools18/bin/sqlcmd \
    -S ${DB_SERVER_URL} \
    -U ${DB_USER} \
    -P "${DB_PASS}" \
    -d master \
    -C -i /sql/"$1".sql
