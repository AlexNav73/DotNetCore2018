FROM rust:1.29.1-slim-stretch

WORKDIR /app
COPY . .
RUN mkdir ./output
VOLUME ./output
RUN ["rustup", "default", "nightly-2018-10-02"]
RUN ["rustup", "target", "add", "wasm32-unknown-unknown"]
RUN ["cargo", "+nightly", "install", "wasm-bindgen-cli"]
RUN ["cargo", "+nightly", "build", "--target", "wasm32-unknown-unknown", "--release"]
RUN ["wasm-bindgen", "./target/wasm32-unknown-unknown/release/ui.wasm", "--out-dir", "./output"]