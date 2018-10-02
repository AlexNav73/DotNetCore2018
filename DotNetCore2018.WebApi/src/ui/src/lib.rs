use js_sys::Promise;
use wasm_bindgen::prelude::*;

macro_rules! console_log {
    ($($t:tt)*) => (web_sys::console::log_1(&format_args!($($t)*).to_string().into()))
}

mod jquery;
mod fetch;

pub use self::jquery::*;
use self::fetch::{Fetch, Method};

#[global_allocator]
static ALLOC: wee_alloc::WeeAlloc = wee_alloc::WeeAlloc::INIT;

#[wasm_bindgen]
pub fn ui_category_delete(id: u32) -> Promise {
    console_error_panic_hook::set_once();

    Fetch::new(Method::Delete, "../api/v1/categoryapi/delete")
        .with_query_params(vec![
            ("id", id.to_string())
        ])
        .send()
}