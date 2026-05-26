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

### Why Use Subject?
Subjects are useful for event streams where you only care that an event occurred, not the current state.

### Subject Examples
- refresh requested
- modal opened
- notification triggered
- websocket event

### BehaviorSubject Examples
- current user
- shopping cart
- selected account
- application settings

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
next() emits or publishes a new value to subscribers.

```typescript
cartItems$.next(updatedCart);
```

---

## What is NgRx?
NgRx is a Redux-style state management library for Angular that centralizes application state into a predictable store using actions, reducers, selectors, and effects.

### Why Use It?
Useful for large Angular applications with complex shared state.

### Core Pieces
- Store
- Actions
- Reducers
- Effects
- Selectors

### Tradeoff
NgRx improves predictability and debugging but can introduce significant boilerplate.

---

## What is a reducer?
A reducer is a pure function that takes current state + action and returns updated state.

### Important Concept
Reducers should avoid side effects and remain predictable.

```typescript
on(addToCart, (state, action) => ({
   ...state,
   cart: [...state.cart, action.item]
}))
```

---

## What are actions?
Actions are events or payloads describing something that happened in the application.

### Examples
- addToCart
- loginSuccess
- loadOrders

---

## What are selectors?
Selectors are reusable functions used to retrieve or derive specific pieces of state from a centralized store.

---

## What are effects?
Effects handle asynchronous operations and side effects.

### Examples
- HTTP requests
- logging
- queue publishing
- navigation

---

## What are signals in Angular?
Signals are Angular's newer reactive state primitives with automatic dependency tracking.

```typescript
count = signal(0);
```

---

## What is change detection?
Angular's mechanism for tracking and updating the UI when application state changes.

---

## What are standalone components?
Standalone components are Angular components that do not require NgModules and can declare dependencies directly.

### Example
```typescript
@Component({
  standalone: true,
  imports: [CommonModule]
})
```

---

## What are observables?
Observables are asynchronous streams of data/events that subscribers can react to over time.

### Common Uses
- HTTP requests
- websocket streams
- reactive state
- event handling

---

## What is Redux?
Redux is a predictable centralized state management pattern where application state is stored in a single store and updated through dispatched actions and reducers.

### Flow
UI -> dispatch action -> reducer updates store -> UI reacts

---

## What is a side effect?
A side effect occurs when a function modifies external state or interacts with systems outside its scope.

### Examples
- API calls
- DB writes
- logging
- navigation
- sending emails

### Pure Function
A pure function only computes and returns a value.

---

## What is dependency injection in Angular?
Angular DI injects services and dependencies where needed instead of tightly coupling implementations.

### Benefits
- testability
- maintainability
- reuse
- separation of concerns

---

## Constructor vs ngOnInit

### Constructor
Used for dependency injection and object setup.

### ngOnInit
Runs after Angular initializes component data bindings.

---

# Angular Lifecycle Hooks

## ngOnInit
Runs after Angular initializes component bindings.

Common Uses:
- initial API calls
- setup logic
- subscriptions

---

## ngOnChanges
Runs when input properties change.

---

## ngAfterViewInit
Runs after component view initialization.

Useful for:
- DOM access
- ViewChild logic
- third-party libraries

---

## ngOnDestroy
Runs before component destruction.

Important for cleanup.

### Common Uses
- unsubscribe observables
- cleanup timers
- dispose resources

Example:
```typescript
ngOnDestroy(): void {
  this.subscription.unsubscribe();
}
```

---

## What causes unnecessary Angular re-renders?
- mutable object references
- functions in templates
- excessive change detection
- large component trees
- non-OnPush components
- unnecessary subscriptions

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

---

# Angular ng-* Directives

## *ngIf
Conditionally renders elements.

```html
<div *ngIf="isLoggedIn">
  Welcome
</div>
```

---

## *ngFor
Loops through collections.

```html
<li *ngFor="let order of orders">
  {{ order.id }}
</li>
```

---

## ngClass
Dynamically applies CSS classes.

```html
<div [ngClass]="{ active: isActive }"></div>
```

---

## ngStyle
Dynamically applies styles.

```html
<div [ngStyle]="{ color: textColor }"></div>
```

---

## ngModel
Two-way binding between UI and component.

```html
<input [(ngModel)]="username" />
```

---

## What are interceptors?
Interceptors inspect or modify HTTP requests/responses globally.

### Common Uses
- auth tokens
- logging
- retries
- metrics
- error handling

---

## What are route guards?
Route guards control access to Angular routes.

### Common Uses
- authentication
- authorization
- unsaved changes

---

## Reactive Forms vs Template-Driven Forms

### Reactive Forms
Defined in TypeScript using FormGroup/FormControl.

### Template-Driven Forms
Defined mostly in HTML templates.

### Tradeoff
Reactive forms scale better for complex forms.

---

## What is lazy loading?
Lazy loading delays loading modules/components until needed.

### Benefits
- smaller initial bundle
- faster startup
- improved performance

---

## EventEmitter vs Subject

### EventEmitter
Used primarily for Angular component output events.

### Subject
General RxJS event stream.

---

## What is RxJS?
RxJS is a reactive programming library used heavily by Angular for async streams and event handling.

### Common Operators
- map
- filter
- switchMap
- debounceTime
- combineLatest
- catchError

---

## switchMap vs mergeMap

### switchMap
Cancels previous observable subscriptions.

Useful for:
- search/autocomplete
- latest request wins

### mergeMap
Allows multiple concurrent subscriptions.

Useful for:
- parallel processing
- bulk operations

---

## Why use trackBy in ngFor?
trackBy improves rendering performance by helping Angular identify list items efficiently.

Example:
```typescript
trackById(index: number, item: Order) {
  return item.id;
}
```

---

## Common Angular Performance Optimizations
- OnPush
- lazy loading
- trackBy
- memoization
- smaller component trees
- avoiding template functions
- virtual scrolling
