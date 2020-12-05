function bind(f, x, ...)
    return function(...)
        f(x, ...)
    end
end