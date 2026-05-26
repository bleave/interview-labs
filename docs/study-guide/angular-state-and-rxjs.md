# Angular State & RxJS

## What controls state in Angular?
Angular state can exist at multiple levels depending on scope and complexity.

Common approaches:
- component variables
- services
- RxJS
- BehaviorSubject
- Signals
- NgRx
- router state
- form state

---

## Subject vs BehaviorSubject

### Subject
Only emits new values after subscription.

### BehaviorSubject
Retains the latest value and immediately emits it to new subscribers.

### Key Concept
Subject = future events only.
BehaviorSubject = current value + future events.

### Subject Example
```typescript
refreshRequested$ = new Subject<void>();

refreshRequested$.next();
```

### BehaviorSubject Example
```typescript
currentUser$ = new BehaviorSubject<User | null>(null);

currentUser$.next(user);
```

---

## What does next() do?
next() emits a new value to subscribers.

```typescript
cartItems$.next(updatedCart);
```

---

## What is NgRx?
NgRx is a Redux-style state management library for Angular.

Core concepts:
- Store
- Actions
- Reducers
- Effects
- Selectors

---

## What is a reducer?
A reducer is a pure function that takes current state + action and returns updated state.

```typescript
on(addToCart, (state, action) => ({
  ...state,
  cart: [...state.cart, action.item]
}))
```

---

## What are effects?
Effects handle async work and side effects.

Examples:
- HTTP calls
- logging
- navigation
- queue publishing

---

## What are Angular signals?
Signals are Angular's newer reactive state primitives with automatic dependency tracking.

```typescript
count = signal(0);
```

---

## What causes unnecessary Angular re-renders?
- mutable object references
- functions in templates
- excessive change detection
- large component trees
- non-OnPush components

---

## What is OnPush?
OnPush reduces unnecessary change detection by only rerendering when:
- input references change
- events occur
- observables/signals emit

---

## What is the async pipe?
Automatically subscribes/unsubscribes observables in templates.

```html
<div>{{ user$ | async }}</div>
```
