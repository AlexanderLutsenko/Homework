import java.util.*;

public class BinSearchTree implements Iterable<Integer> {

	public TreeIterator iterator() {
		return new TreeIterator();
	}
	
	public class TreeIterator implements Iterator<Integer> {
		public TreeIterator() {
			stack = new Stack();
		}
		public Integer next() {
			if (!hasNext()) {
				throw new NoSuchElementException();
			}
			Element currentElem = stack.pop();
			Integer result = currentElem.getValue();
			if (currentElem.getRight() != null) {
				stack.push(currentElem.getRight());
			}
			if (currentElem.getLeft() != null) {
				stack.push(currentElem.getLeft());
			}
			return result;
		}
		public boolean hasNext() {
			return !(stack.isEmpty());			
		}
		public void remove() {
			throw new UnsupportedOperationException();
		}
		
		private class Stack {
			Stack() {
				if (root != null) {
					head = new Node(null, root);
				}
			}
			void push(Element currentElem) {
				head = new Node(head, currentElem);
			}
			Element pop() {
				if (head == null) return null;
				Element value = head.value;
				head = head.next;
				return value;
			}		
			boolean isEmpty() {
				return head == null;
			}
			private class Node {
				private Element value;
				private Node next;
				private Node(Node next, Element value) {
					this.next = next;
					this.value = value;
				}
			}
			private Node head;	
		}
		private Stack stack;
	}

	public void addElement(Integer value) {
		if(root == null) {
			root = new Element(value);
		} else {
			Element currentElem = root;
			Element parentElem = null;
			while (currentElem != null) {
				parentElem = currentElem;
				if (value.compareTo(currentElem.value) > 0) {
					currentElem = currentElem.right;
				} else {
					currentElem = currentElem.left;
				}
			}			
			if (value.compareTo(parentElem.value) > 0) {
				parentElem.addChildR(value);
			} else {
				parentElem.addChildL(value);
			}
		}
	}
	
	public String toLSF() {
		return treeToLSF(root);
	}
	
	
	private String treeToLSF(Element currentElem) {
		if (currentElem == null) {
			return "*";
		} else {
			if (currentElem.left != null || currentElem.right != null) { 
				return currentElem.value+"("+treeToLSF(currentElem.left)+","+treeToLSF(currentElem.right)+")";
			} else {
				return currentElem.value+"";
			}
		}		
	}

	private class Element {
		Element(Integer value) {
			this.value = value;
		}
		
		Element getLeft() {
			return this.left;
		}
		
		Element getRight() {
			return this.right;
		}
		
		Integer getValue() {
			return this.value;
		}
		
		void addChildL(Integer value) {
			this.left = new Element(value); 
		}
		
		void addChildR(Integer value) {
			this.right = new Element(value); 
		}
	
		
		private Integer value;
		private Element left;
		private Element right;		
	}
	
	private Element root;	
}