#!/bin/bash

# VALKYRIE PLATINUM - Quick Launcher
echo "🚀 Starting VALKYRIE.PLATINUM Development Environment..."

# NVM'i yükle (npm komutunun çalışması için şart)
export NVM_DIR="$HOME/.nvm"
if [ -s "$NVM_DIR/nvm.sh" ]; then
    \. "$NVM_DIR/nvm.sh"
    # Son sürümü veya projenin ihtiyacı olanı seç
    nvm use --lts || nvm use default
fi

# Check if node_modules exist
if [ ! -d "node_modules" ]; then
    echo "📦 node_modules not found. Installing dependencies..."
    npm install
fi

echo "🛠️ Launching Environment..."
npm run dev
