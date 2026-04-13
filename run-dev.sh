#!/bin/bash

set -euo pipefail

ROOT_DIR="$(cd "$(dirname "$0")" && pwd)"
API_PID=""

cleanup() {
  if [[ -n "$API_PID" ]] && kill -0 "$API_PID" 2>/dev/null; then
    kill "$API_PID" 2>/dev/null || true
    wait "$API_PID" 2>/dev/null || true
  fi
}

trap cleanup EXIT INT TERM

cd "$ROOT_DIR"

echo "Levantando Login_MinimalAPI en http://localhost:5106 ..."
ASPNETCORE_ENVIRONMENT=Development dotnet run --project Login_MinimalAPI --no-launch-profile --urls http://localhost:5106 >/tmp/login_minimalapi.log 2>&1 &
API_PID=$!

for _ in {1..20}; do
  if lsof -nP -iTCP:5106 -sTCP:LISTEN >/dev/null 2>&1; then
    break
  fi
  sleep 1
done

if ! lsof -nP -iTCP:5106 -sTCP:LISTEN >/dev/null 2>&1; then
  echo "No se pudo iniciar Login_MinimalAPI. Revisa /tmp/login_minimalapi.log"
  exit 1
fi

echo "Levantando Proyecto_Musica_GH en http://localhost:5129 ..."
ASPNETCORE_ENVIRONMENT=Development dotnet run --project Proyecto_Musica_GH --no-launch-profile --urls http://localhost:5129
