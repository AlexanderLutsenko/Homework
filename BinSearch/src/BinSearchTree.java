
import java.util.*;

public class BinSearchTree implements Iterable<Integer> {

    @Override
    public TreeIterator iterator() {
        return new TreeIterator();
    }

    public class TreeIterator implements Iterator<Integer> {

        public TreeIterator() {
            stack = new Stack<>(root);
        }

        @Override
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

        @Override
        public boolean hasNext() {
            return !(stack.isEmpty());
        }

        @Override
        public void remove() {
            throw new UnsupportedOperationException();
        }
        private Stack<Element> stack;
    }

    public void addElement(Integer value) {
        if (root == null) {
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

    public boolean findElement(int value) {
        Element currentElem = root;
        while (currentElem != null && currentElem.getValue() != value) {
            if (value > currentElem.getValue()) {
                currentElem = currentElem.right;
            } else {
                currentElem = currentElem.left;
            }
        }
        return currentElem != null;
    }

    public String toLSF() {
        return treeToLSF(root);
    }

    private String treeToLSF(Element currentElem) {
        if (currentElem == null) {
            return "*";
        } else {
            if (currentElem.left != null || currentElem.right != null) {
                return currentElem.value + "(" + treeToLSF(currentElem.left) + "," + treeToLSF(currentElem.right) + ")";
            } else {
                return currentElem.value + "";
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