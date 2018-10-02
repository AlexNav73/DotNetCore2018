use js_sys::Promise;
use wasm_bindgen::prelude::*;
use wasm_bindgen_futures::future_to_promise;
use wasm_bindgen_futures::JsFuture;
use web_sys::{Request, RequestInit};

macro_rules! console_log {
    ($($t:tt)*) => (web_sys::console::log_1(&format_args!($($t)*).to_string().into()))
}

mod jquery;

pub use self::jquery::*;

#[global_allocator]
static ALLOC: wee_alloc::WeeAlloc = wee_alloc::WeeAlloc::INIT;

#[wasm_bindgen]
pub fn ui_category_delete(id: u32) -> Promise {
    console_error_panic_hook::set_once();

    let url = format!("../api/v1/categoryapi/delete?id={}", id);

    let mut init = RequestInit::new();
    init.method("DELETE");
    let request = Request::new_with_str_and_init(&url, &init).unwrap();
    let window = web_sys::window().unwrap();
    let request_promise = window.fetch_with_request(&request);

    future_to_promise(JsFuture::from(request_promise))
}