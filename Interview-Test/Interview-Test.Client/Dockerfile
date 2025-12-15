# Build stage
FROM node:18-alpine AS builder

WORKDIR /app

# Copy package files
COPY Interview-Test.Client/package*.json ./

# Install dependencies
RUN npm install

# Copy application source
COPY Interview-Test.Client/ .

# Build the Angular application
RUN npm run build

# Runtime stage
FROM node:18-alpine

WORKDIR /app

# Install serve to run the application
RUN npm install -g serve

# Copy built application from builder stage
COPY --from=builder /app/dist/interview-test-client ./dist/interview-test-client

# Expose port
EXPOSE 4200

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD node -e "require('http').get('http://localhost:4200', (r) => {if (r.statusCode !== 200) throw new Error(r.statusCode)})"

# Start the application
CMD ["serve", "-s", "dist/interview-test-client", "-l", "4200"]
