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

macro_rules! delete_api_call {
    ($name:ident, $controller:expr, $action:expr) => {
        #[wasm_bindgen]
        pub fn $name(id: u32) -> Promise {
            console_error_panic_hook::set_once();

            Fetch::new(Method::Delete, concat!("/api/v1/", $controller, "/", $action))
                .with_query_params(vec![
                    ("id", id.to_string())
                ])
                .send()
        }
    }
}

delete_api_call!(ui_category_delete, "categories", "delete");
delete_api_call!(ui_product_delete, "products", "delete");
delete_api_call!(ui_supplier_delete, "suppliers", "delete");