#!/bin/env bash
# shellcheck disable=1091,2086

set -euo pipefail

. .env

CONTAINER_NETWORK=$(basename "$(pwd)")_mssql_server_network
DB_NAME_OR_EMPTY=$(if [ -n "${DB_NAME}" ]; then echo "-d ${DB_NAME}"; fi)
DB_SERVER_URL="${DB_HOST},${DB_PORT}"

docker run \
    -it --rm \
    --network "${CONTAINER_NETWORK}" \
    --name go-sqlcmd \
    ghcr.io/bonddim/go-sqlcmd \
    -S "${DB_SERVER_URL}" \
    -U "${DB_USER}" \
    -P "${DB_PASS}" \
    ${DB_NAME_OR_EMPTY}
