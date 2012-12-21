public class Stack<E> {

    public Stack(E headE) {
        if (headE != null) {
            this.head = new Node(null, headE);
        }
    }

    public void push(E currentElem) {
        head = new Node(head, currentElem);
    }

    public E pop() {
        if (head == null) {
            return null;
        }
        E value = head.value;
        head = head.next;
        return value;
    }

    public boolean isEmpty() {
        return head == null;
    }

    private class Node {
        private Node(Node next, E value) {
            this.next = next;
            this.value = value;
        }
        private E value;
        private Node next;

    }
    private Node head;
}