use js_sys::Promise;
use web_sys::{Request, RequestInit};
use serde::ser::Serialize;
use serde_urlencoded::to_string;

use std::borrow::Cow;

pub struct Fetch<'a> {
    uri: Cow<'a, str>,
    method: Method,
}

pub enum Method {
    Get,
    Post,
    Delete,
}

impl<'a> Fetch<'a> {
    pub fn new<U>(method: Method, uri: U) -> Self 
        where U: Into<Cow<'a, str>>
    {
        Self { method, uri: uri.into() }
    }

    pub fn with_query_params<T: Serialize>(mut self, params: T) -> Self {
        self.uri = format!("{}?{}", self.uri, to_string(params).expect("invalid url params")).into();
        self
    }

    pub fn send(self) -> Promise {
        let mut init = RequestInit::new();
        init.method(self.method.as_ref());

        let request = Request::new_with_str_and_init(&self.uri, &init).unwrap();
        let window = web_sys::window().unwrap();
        let request_promise = window.fetch_with_request(&request);

        request_promise
    }
}

impl AsRef<str> for Method {
    fn as_ref(&self) -> &'static str {
        match self {
            Method::Get => "GET",
            Method::Post => "POST",
            Method::Delete => "DELETE",
        }
    }
}