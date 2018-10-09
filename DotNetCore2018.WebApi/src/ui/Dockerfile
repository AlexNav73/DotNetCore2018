FROM rust

# VOLUME /ui
WORKDIR /ui

# COPY ./build_ui.sh .

RUN rustup default nightly-2018-10-02
RUN rustup target add wasm32-unknown-unknown
RUN cargo +nightly-2018-10-02 install wasm-bindgen-cli

# ENTRYPOINT ["/bin/sh", "-c", "build_ui.sh"]