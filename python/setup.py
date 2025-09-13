import setuptools

with open("README.md", "r") as fh:
    long_description = fh.read()

setuptools.setup(
    name="doublets-gql",
    version="0.2.0",
    author="LinksPlatform Contributors",
    author_email="konard@yandex.ru",
    description="Python Doublets Adapter via GraphQL client - Native Python interface for Doublets operations",
    long_description=long_description,
    long_description_content_type="text/markdown",
    url="https://github.com/linksplatform/Data.Doublets.Gql",
    packages=setuptools.find_packages(),
    license="LGPLv3",
    keywords="Doublets, GraphQL, Links, Associations, Data Structure, Database",
    classifiers=[
        "Development Status :: 4 - Beta",
        "Intended Audience :: Developers",
        "Topic :: Database",
        "Topic :: Software Development :: Libraries :: Python Modules",
        "Programming Language :: Python :: 3",
        "Programming Language :: Python :: 3.8",
        "Programming Language :: Python :: 3.9",
        "Programming Language :: Python :: 3.10",
        "Programming Language :: Python :: 3.11",
        "Programming Language :: Python :: 3.12",
        "License :: OSI Approved :: GNU Lesser General Public License v3 (LGPLv3)",
        "Operating System :: OS Independent",
    ],
    project_urls={
        "Homepage": "https://github.com/linksplatform/Data.Doublets.Gql",
        "Repository": "https://github.com/linksplatform/Data.Doublets.Gql",
        "Issues": "https://github.com/linksplatform/Data.Doublets.Gql/issues",
        "Documentation": "https://github.com/linksplatform/Data.Doublets.Gql/tree/main/python",
    },
    python_requires=">=3.8",
    install_requires=[
        'gql>=3.0.0',
        'aiohttp>=3.8.0',
    ],
    extras_require={
        'dev': [
            'pytest>=7.0.0',
            'pytest-asyncio>=0.21.0',
            'pytest-cov>=4.0.0',
        ],
        'docs': [
            'sphinx>=5.0.0',
            'sphinx-rtd-theme>=1.0.0',
        ],
    },
)
