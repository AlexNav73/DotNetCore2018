#![allow(dead_code)]
#![allow(unused_macros)]

use js_sys::{JSON, Promise};
use wasm_bindgen::prelude::*;
use wasm_bindgen::JsCast;
use wasm_bindgen_futures::{JsFuture, future_to_promise};
use web_sys::Response;
use futures::Future;

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

#[wasm_bindgen]
pub fn ui_show_list(container: Element) -> Promise {
    console_error_panic_hook::set_once();

    let future = JsFuture::from(Fetch::new(Method::Get, "/api/v1/categories").send())
        .and_then(|resp| {
            assert!(resp.is_instance_of::<Response>());
            let resp: Response = resp.dyn_into().unwrap();
            resp.json()
        })
        .and_then(|promise| JsFuture::from(promise))
        .and_then(move |json| {
            let text: String = JSON::stringify(&json).unwrap().into();
            container.set_text(&text);
            futures::future::ok(text.into())
        });

    future_to_promise(future)
}