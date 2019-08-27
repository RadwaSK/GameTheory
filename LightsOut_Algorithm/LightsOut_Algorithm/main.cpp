
#include "Matrix.cpp"
#include <vector>
#include <utility>    // For std::pair, std::make_pair
#include <algorithm>  // For std::fill, std::swap_ranges, std::swap,
//     std::transform
#include <stdexcept>  // For std::runtime_error
#include <functional> // For std::not_equal_to
#include <numeric>    // For std::accumulate
#include <iostream>
using namespace std;
///////////////Used Functions////////////////////
template <size_t N>
size_t RowMajorIndex(size_t i, size_t j);

template <size_t M, size_t N>
Vector<M * N, bool> LinearizePuzzle(const Matrix<M, N, bool>& puzzle);

template <size_t MN>
Vector<MN, bool> BackSubstitute(const Matrix<MN, MN, bool>& toggle, const Vector<MN, bool>& puzzle);

template <size_t M, size_t N>
vector< pair<size_t, size_t> >SolutionVectorToPairs(const Vector<M * N, bool>& solution);

template <size_t M, size_t N>
vector< pair<size_t, size_t>> SolveLightsOut(const Matrix<M, N, bool>& puzzle);

////////////////////////////////////////////////////////////////////

/////////////////Main program /////////////
int main() {
	bool x;
	Matrix<5, 5, bool>puzzle;
	cout << "Please enter your initial state as instructed:\nEnter the values, space separated as in a matrix, 1 for on-lid and 0 for off-lid\n";
	for (int i = 0; i < 5; i++) {
		for (int j = 0; j < 5; j++) {
			cin >> x;
			puzzle[i][j] = x;
		}
	}
	try {
		vector<pair<size_t, size_t> > Solution = SolveLightsOut(puzzle);
		cout << "Press on lids with following coordinates to solve the game:\n";
		for (size_t i = 0; i < Solution.size(); i++)
			cout << "( " << Solution[i].first + 1 << " , " << Solution[i].second + 1 << " )" << endl;
	}
	catch (exception) {
		cout << "Sorry! It seems like there is no solution for such state. :(";
	}
}


	/*
	  * Function: RowMajorIndex<N>(size_t i, size_t j)
	  * --------------------------------------------------------------------------
	  * Given an M x N matrix and a location (i, j) within that matrix, returns
	  * the index at which that location may be found in a row-major ordering of
	  * the elements of the array.  Since each step across a column just moves one
	  * step further in the linearization and each step across a row moves past
	  * the N elements of the row, this is given by i * N + j.
	  */
	template <size_t N>
	size_t RowMajorIndex(size_t i, size_t j) {
		return i * N + j;
	}



	/* Function: LinearizePuzzle(const Matrix<M, N, bool>& puzzle);
	 * -------------------------------------------------------------------------
	 * Linearizes a Lights Out puzzle into a vector in row-major order.
	 */
	template <size_t M, size_t N>
	Vector<M * N, bool> LinearizePuzzle(const Matrix<M, N, bool>& puzzle) {
		/* The Vector constructor allows us to provide a set of iterators defining
		 * the elements, and the Matrix type exports iterators to traverse the
		 * elements in row-major order.  We just connect the two here.
		 */
		return Vector<M * N, bool>(puzzle.begin(), puzzle.end());
	}

	/*
	 * Using back-substitution on the row-echelon matrix toggle augmented with
	 * the column puzzle, returns a vector such that V a = G, where V is the
	 * row-reduced toggle matrix and G is the puzzle vector.  If the system has
	 * no solution, a std::runtime_error exception is raised.
	 */
	template <size_t MN>
	Vector<MN, bool> BackSubstitute(const Matrix<MN, MN, bool>& toggle,
		const Vector<MN, bool>& puzzle) {
		/* Many of the puzzles we'll be solving have multiple solutions.  Because
		 * of this, we need to pick some concrete one as our answer.  Since back-
		 * substitution always assigns the last variables values before the first,
		 * we will maintain our candidate solution vector initialized to all zero
		 * values, then will update them accordingly.
		 */
		Vector<MN, bool> result;
		std::fill(result.begin(), result.end(), false);

		/* Iterate from the bottom of the matrix to the top, updating our answer.
		 * Because we want to iterate using unsigned values, we'll use a cute for
		 * loop.  We will initialize the loop counter to MN, which is out of
		 * bounds, and will then have our loop check include a test-and-decrement
		 * to back it up one level.
		 */
		for (size_t row = MN; row-- != 0; ) {
			/* Scan across the row to find the pivot, if one exists. */
			size_t pivot(-1);

			for (size_t col = 0; col < MN; ++col) {
				if (toggle[row][col]) {
					pivot = col;
					break;
				}
			}

			/* There are now two cases to check.  If this row is all zeros (i.e.
			 * there's no pivot), we must check that the puzzle vector is false
			 * here, since otherwise no solution exists.
			 */
			if (pivot == size_t(-1)) {
				if (puzzle[row])
					throw std::runtime_error("Puzzle has no solution.");
			}
			else {
				/* Otherwise, update the value of this particular variable by using
				 * the information in the rest of the row.  To do this, we know from
				 * the positions of the remaining ones in the row and the value of the
				 * puzzle vector that the value we should give to this variable is
				 * one that satisfies
				 *
				 *     x_j + r[j+1] x_j+1 + ... + r[mn] x_mn = puzzleValue
				 *
				 * Subtracting those later values, we get
				 *
				 *     x_j = -r[j+1] x_j+1 + ... + -r[mn] x_mn + puzzleValue
				 *
				 * But because we're working in GF(2), -x == x for all x, so we get
				 * that
				 *     x_j = r[j+1] x_j+1 + ... + r[mn] x_mn + puzzleValue
				 *
				 * In other words, we just iterate across the rest of the row, XORing
				 * the values together with the puzzle value and then store the
				 * result.
				 */
				result[row] = puzzle[row];
				for (size_t col = pivot + 1; col < MN; ++col)
					result[row] = (result[row] != (toggle[row][col] & result[col]));
			}
		}

		return result;
	}


	// * Given a vector containing the solution to a Lights Out puzzle encoded as
	// * a column vector, expands the solution into a list of pairs.
	template <size_t M, size_t N>
	std::vector< std::pair<size_t, size_t> >SolutionVectorToPairs(const Vector<M * N, bool>& solution) {
		std::vector< std::pair<size_t, size_t> > result;

		/* Iterate across the bits, rehydrating each. */
		for (size_t i = 0; i < M * N; ++i) {
			if (solution[i]) {
				/* Convert from an index back to a row/column pair.  To do this, we
				 * use the quotient and the remainder of the raw index when divided by
				 * the width of the matrix.
				 */
				result.push_back(std::make_pair(i / N, i % N));
			}
		}

		return result;
	}


	/* Actual implementation of the Lights Out solver. */
	template <size_t M, size_t N>
	std::vector< std::pair<size_t, size_t>> SolveLightsOut(const Matrix<M, N, bool>& puzzle) {
		// Constructing C matrix 
		bool C[25][25] = {
		{1 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0, 0, 0,0 ,0, 0 ,0 ,0 ,0 ,0},
		{0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0, 0, 0, 0, 0, 0, 0,0},
		{1 ,1 ,0 ,1 ,0 ,0 ,0 ,0 ,0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		{1 ,0 ,1 ,1 ,0 ,1 ,0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0},
		{1, 1 ,0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0, 0},
		{0 ,1, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 1, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0},
		{0 ,1 ,0 ,0 ,0 ,1 ,1 ,1 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0},
		{0 ,0 ,1 ,0 ,0 ,0 ,1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0, 0, 0},
		{1, 0, 1, 0, 1, 1, 0 ,1 ,0 ,1 ,0 ,0 ,0 ,0 ,0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 0, 1, 1, 1, 1, 0, 0, 1, 0, 0, 1 ,0 ,0, 0, 0, 0 ,0 ,0, 0, 0 ,0, 0 ,0, 0},
		{1,0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 ,0, 0 ,0 ,0 ,0},
		{0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0 ,0 ,0 ,0 ,0},
		{0, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		{0, 1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 1, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1, 1, 0 ,1 ,0 ,1, 0, 1, 0 ,0 ,0 ,0 ,0},
		{1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0 ,0},
		{0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0, 1, 0, 1, 0, 1, 1, 1 ,0 ,1 ,0 ,0 ,0, 0},
		{0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 1, 1, 1, 0, 1, 0 ,0},
		{0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 0},
		{0, 1, 1, 1, 0 ,1 ,0 ,1 ,0 ,1, 1, 1, 0 ,1 ,1, 1 ,0 ,1, 0 ,1 ,0 ,1 ,1, 1, 0},
		{1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1 ,0 ,1 ,0 ,1 ,1 ,0, 1, 0 ,1}
		};


		/* Linearize the puzzle into a vector so that we can swap its rows in the
		 * same way that we would sway the rows of the matrix.
		 */
		Vector<M * N, bool> puzzleVector = LinearizePuzzle(puzzle);

		//Multiply C by S and save result at C_S
		Vector <M*N, bool>C_S;
		for (int i = 0; i < 25; i++) {
			bool sum = 0;
			for (int j = 0; j < 25; j++)
				sum ^= C[i][j] & puzzleVector[j];
			C_S.at(i) = sum;
		}

		// Construct U matrix
		Matrix<25, 25, bool>U;
		bool temp[25][25] = {
			{1,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,1,1,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,1,1,1,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,1,0,1,1,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,1,1,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,1,1,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,1,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,1,1,0,1,1,0,1,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,1,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,0,0,1,0,1,1,1,0,0,0,1,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,1,1,0,0,1,1,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,1,1,0,1,0,1,0,0},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,1,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,1,0,1},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,1,1,1,0},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,1,1,0},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,1},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
		};
		for (int i = 0; i < 25; i++)
			for (int j = 0; j < 25; j++)
				U[i][j] = temp[i][j];


		// obtain a solution to the original problem. 
		Vector<M * N, bool> solution = BackSubstitute(U, C_S);

		/* Convert the solution vector into a list of the buttons to toggle. */
		return SolutionVectorToPairs<M, N>(solution);
	}



