echo Starting building rust crate ...
cargo.exe +nightly build --target wasm32-unknown-unknown
echo Generating bindings for wasm module ...
wasm-bindgen .\target\wasm32-unknown-unknown\debug\ui.wasm --out-dir ..\..\src\ --no-typescript
echo Done