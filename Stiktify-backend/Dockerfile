##### Dockerfile #####

# ===== BUILD STAGE =====
FROM node:18-alpine AS builder

# Set working directory
WORKDIR /app

# Copy and install dependencies
COPY package*.json ./
RUN npm install

# Copy source code and build
COPY . .
RUN npm run build

# ===== RUN STAGE =====
FROM node:18-alpine

# Set environment
ARG NODE_ENV=production
ENV NODE_ENV=${NODE_ENV}

# Set working directory
WORKDIR /usr/src/app

# Install only production dependencies
COPY package*.json ./
RUN npm install --only=production

# Copy built app from builder stage
COPY --from=builder /app/dist ./dist

# Copy any other needed files (optional)
COPY --from=builder /app/node_modules ./node_modules

# Run app
CMD ["node", "dist/main.js"]
